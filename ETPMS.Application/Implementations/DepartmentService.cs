using System;
using System.Collections.Generic;
using ETPMS.Application.Contracts;
using ETPMS.Application.Models;
using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Repository;
using System.Linq;
using ETPMS.Application.DTOs;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Application.Enums;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class DepartmentService : ETPMSBaseService<UM_DEPARTMENT>, IDepartmentService
    {
        public DepartmentService(IRepository<UM_DEPARTMENT> repository) : base(repository)
        {
        }

        public List<SimplifiedDepartmentInfo> GetAllDepartments()
        {
            var simplifiedDepartments = from k in base.Repository.GetAll()
                                        where !k.IS_DELETED
                                        select new SimplifiedDepartmentInfo
                                        {
                                            DepartmentId = k.ID,
                                            FatherDepartmentId = k.FATHER_DEPARTMENT_ID,
                                            DepartmentCode = k.DEPARTMENT_CODE,
                                            DepartmentName = k.DEPARTMENT_NAME,
                                            DepartmentShortName = k.DEAPRTMENT_SHORT_NAME
                                        };

            return simplifiedDepartments.ToList();
        }

        public PagedList<DepartmentDto> GetDepartments(DateTime dateFrom, DateTime dateTo, PageDescriptor pageDescriptor)
        {
            var totalCount = 0;
            var predicate = PredicateBuilder.True<UM_DEPARTMENT>()
                .And(s => s.CREATE_TIME >= dateFrom && s.CREATE_TIME <= dateTo && !s.IS_DELETED);
            var items = base.Repository.GetPaged(predicate, out totalCount,
            pageDescriptor.PageIndex, pageDescriptor.PageSize, pageDescriptor.SortField, pageDescriptor.IsAscending)
            .MapToList<UM_DEPARTMENT, DepartmentDto>();

            return new PagedList<DepartmentDto>(pageDescriptor.PageIndex, pageDescriptor.PageSize)
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public DepartmentTreeDescriptor GetDepartmentTree()
        {
            var departmentEntities = this.Repository.GetByWhere(k => !k.IS_DELETED).ToList();
            var lv1DepartmentIds = (from k in departmentEntities where k.FATHER_DEPARTMENT_ID == 0 select k.ID).ToList();
            var rootDepartment = new DepartmentTreeDescriptor { DepartmentId = 0, DepartmentCode = "Root", DepartmentName = "华北电力研究院", ImageUrl = "pf-user-dpt-sprite" };

            if (lv1DepartmentIds.Any())
            {
                rootDepartment.Expanded = true;
                rootDepartment.HasChildDepartments = true;
                rootDepartment.ChildDepartments = new List<DepartmentTreeDescriptor>();
                lv1DepartmentIds.ForEach(s => rootDepartment.ChildDepartments.Add(this.GetDepartmentTree(s, departmentEntities)));
            }

            return rootDepartment;
        }

        public List<DepartmentNodeDescriptor> GetChildDepartmentNodeByDepartmentId(int departmentId)
        {
            var childDepartmentNodes = new List<DepartmentNodeDescriptor>();
            var departmentEntities = base.Repository.GetByWhere(s => !s.IS_DELETED).ToList();
            var childDepartmentEntities = departmentEntities.FindAll(s => s.FATHER_DEPARTMENT_ID == departmentId);
            childDepartmentEntities.ForEach(k =>
            {
                childDepartmentNodes.Add(new DepartmentNodeDescriptor
                {
                    DepartmentId = k.ID,
                    DepartmentCode = k.DEPARTMENT_CODE,
                    DepartmentName = k.DEPARTMENT_NAME,
                    SortIndex = k.DEPARTMENT_INDEX,
                    Expanded = departmentId == 0,
                    FatherDepartmentId = departmentId,
                    HasChildDepartments = departmentEntities.Any(s => s.FATHER_DEPARTMENT_ID == k.ID),
                    ImageUrl = departmentEntities.Any(s => s.FATHER_DEPARTMENT_ID == k.ID) ? "pf-user-dpt-sprite" : "pf-user-dpt-sprite dpt-sprite-no-child"
                });
            });

            childDepartmentNodes.Sort(new EntityComparer<DepartmentNodeDescriptor>((x, y) => x.SortIndex.CompareTo(y.SortIndex)));
            return childDepartmentNodes;
        }



        public DepartmentDto GetDepartmentById(int departmentId)
        {
            var departmentEntity = this.Repository.GetById(departmentId);
            return departmentEntity.MapTo<DepartmentDto>();
        }

        public OperationResult AddDepartment(DepartmentDto departmentDto)
        {
            var item = base.Repository.GetFirstOrDefualt(k => (k.DEPARTMENT_CODE == departmentDto.DEPARTMENT_CODE || k.DEPARTMENT_NAME == departmentDto.DEPARTMENT_NAME) && !k.IS_DELETED);
            if (item != null && item.DEPARTMENT_CODE == departmentDto.DEPARTMENT_CODE)
                return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"添加部门失败,已经存在部门编码为:{departmentDto.DEPARTMENT_CODE}的部门~" };
            else if (item != null && item.DEPARTMENT_NAME == departmentDto.DEPARTMENT_NAME)
                return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"添加部门失败,已经存在部门名称为:{departmentDto.DEPARTMENT_NAME}的部门~" };
            else
            {
                var departmentEntity = departmentDto.MapTo<UM_DEPARTMENT>();
                departmentEntity.IS_DELETED = false;
                departmentEntity.DEAPRTMENT_SHORT_NAME = departmentEntity.DEAPRTMENT_SHORT_NAME ?? string.Empty;
                departmentEntity.DEPARTEMNT_DESCRIPTION = departmentEntity.DEPARTEMNT_DESCRIPTION ?? string.Empty;
                base.Repository.Add(departmentEntity);
                return new OperationResult
                {
                    Data = new { DepartmentId = departmentEntity.ID },
                    ResultType = OperationResultType.Succed,
                    Message = $"添加部门成功~"
                };
            }
        }

        public OperationResult UpdateDepartment(DepartmentDto departmentDto)
        {
            var departmentEntity = base.Repository.GetById(departmentDto.ID);
            if (departmentEntity == null || departmentEntity.IS_DELETED)
                return new OperationResult { ResultType = OperationResultType.Failed, Message = $"更新部门信息失败,无相应的部门~" };
            else
            {
                var item = base.Repository.GetFirstOrDefualt(k => k.ID != departmentDto.ID && !k.IS_DELETED && (k.DEPARTMENT_CODE == departmentDto.DEPARTMENT_CODE || k.DEPARTMENT_NAME == departmentDto.DEPARTMENT_NAME));
                if (item != null && departmentDto.DEPARTMENT_CODE == departmentDto.DEPARTMENT_CODE)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新部门信息失败,已经存在编码为:{departmentDto.DEPARTMENT_CODE}的部门~" };
                else if (item != null && item.DEPARTMENT_NAME == departmentDto.DEPARTMENT_NAME)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新用户信息失败,已经存在名称为:{departmentDto.DEPARTMENT_NAME}的部门~" };
                else
                {
                    departmentEntity.DEPARTMENT_NAME = departmentDto.DEPARTMENT_NAME;
                    departmentEntity.DEAPRTMENT_SHORT_NAME = departmentDto.DEAPRTMENT_SHORT_NAME ?? string.Empty;
                    departmentEntity.DEPARTEMNT_DESCRIPTION = departmentDto.DEPARTEMNT_DESCRIPTION ?? string.Empty;
                    departmentEntity.DEPARTMENT_INDEX = departmentDto.DEPARTMENT_INDEX;
                    departmentEntity.DEPARTMENT_LEADER_ID = departmentDto.DEPARTMENT_LEADER_ID;
                    departmentEntity.UPDATE_TIME = departmentDto.UPDATE_TIME.Value;
                    departmentEntity.OPERATOR_ID = departmentDto.OPERATOR_ID.Value;
                    base.Repository.Update(departmentEntity);
                    return new OperationResult { ResultType = OperationResultType.Succed, Message = "更新部门信息成功~" };
                }
            }
        }

        public OperationResult DeleteDepartment(DepartmentDto departmentDto)
        {
            var departmentEntity = base.Repository.GetById(departmentDto.ID);
            if (departmentEntity == null || departmentEntity.IS_DELETED)
                return new OperationResult { ResultType = OperationResultType.Failed, Message = $"删除部门信息失败,无相应的部门~" };
            else
            {
                departmentEntity.OPERATOR_ID = departmentDto.OPERATOR_ID.Value;
                departmentEntity.UPDATE_TIME = DateTime.Now;
                departmentEntity.IS_DELETED = true;
                base.Repository.Update(departmentEntity);
                return new OperationResult { ResultType = OperationResultType.Succed, Message = "删除部门成功~" };
            }
        }

        private DepartmentTreeDescriptor GetDepartmentTree(int departmentId, List<UM_DEPARTMENT> allDepartmentEntities)
        {
            DepartmentTreeDescriptor departmentDescriptor = null;
            var departmentEntity = allDepartmentEntities.FirstOrDefault(p => p.ID == departmentId);
            if (departmentEntity != null)
            {
                departmentDescriptor = new DepartmentTreeDescriptor
                {
                    DepartmentId = departmentEntity.ID,
                    DepartmentCode = departmentEntity.DEPARTMENT_CODE,
                    DepartmentName = departmentEntity.DEPARTMENT_NAME,
                    ImageUrl = "pf-user-dpt-sprite",
                    SortIndex = (byte)departmentEntity.DEPARTMENT_INDEX,
                    FatherDepartmentId = departmentEntity.FATHER_DEPARTMENT_ID,
                    Expanded = departmentEntity.FATHER_DEPARTMENT_ID == 0,
                    ChildDepartments = new List<DepartmentTreeDescriptor>(),
                    HasChildDepartments = false
                };
                var allChildMenuEntities = allDepartmentEntities.FindAll(p => p.FATHER_DEPARTMENT_ID == departmentEntity.ID);
                if (allChildMenuEntities.Any())
                {
                    departmentDescriptor.HasChildDepartments = true;
                    allChildMenuEntities.ForEach(k =>
                    {
                        departmentDescriptor.ChildDepartments.Add(this.GetDepartmentTree(k.ID, allDepartmentEntities));
                    });
                }
            }
            return departmentDescriptor;
        }

    }
}
