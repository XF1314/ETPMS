using ETPMS.Application.Contracts;
using ETPMS.Application.Models;
using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System;
using ETPMS.Application.DTOs;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Application.Enums;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class RoleService : ETPMSBaseService<UM_ROLE>, IRoleService
    {
        public RoleService(IRepository<UM_ROLE> repository) : base(repository)
        {

        }

        public PagedList<RoleDto> GetRoles(DateTime dateFrom, DateTime dateTo, PageDescriptor pageDescriptor)
        {
            var totalCount = 0;
            var predicate = PredicateBuilder.True<UM_ROLE>()
                .And(s => s.CREATE_TIME >= dateFrom && s.CREATE_TIME <= dateTo && !s.IS_DELETED);
            var items = base.Repository.GetPaged(predicate, out totalCount,
                pageDescriptor.PageIndex, pageDescriptor.PageSize, pageDescriptor.SortField, pageDescriptor.IsAscending)
                .MapToList<UM_ROLE, RoleDto>();

            return new PagedList<RoleDto>(pageDescriptor.PageIndex, pageDescriptor.PageSize)
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public OperationResult AddRole(RoleDto roleDto)
        {
            var item = base.Repository.GetFirstOrDefualt(k => k.ROLE_CODE == roleDto.ROLE_CODE && !k.IS_DELETED);
            if (item != null && item.ROLE_CODE == roleDto.ROLE_CODE)
                return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"添加角色失败,已经存在编码为:{roleDto.ROLE_CODE}的角色~" };
            else if (item != null && item.ROLE_CODE == roleDto.ROLE_NAME)
                return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"添加角色失败,已经存在名称为:{roleDto.ROLE_NAME}的角色~" };
            else
            {
                var roleEntity = roleDto.MapTo<UM_ROLE>();
                roleEntity.IS_DELETED = false;
                base.Repository.Add(roleEntity);
                return new OperationResult
                {
                    ResultType = OperationResultType.Succed,
                    Message = $"添加角色成功~"
                };
            }
        }

        public OperationResult UpdateRole(RoleDto roleDto)
        {
            var roleEntity = base.Repository.GetById(roleDto.ID);
            if (roleEntity == null || roleEntity.IS_DELETED)
                return new OperationResult { ResultType = OperationResultType.Failed, Message = $"更新角色信息失败,无相应的角色~" };
            else
            {
                var item = base.Repository.GetFirstOrDefualt(k => k.ID != roleDto.ID && !k.IS_DELETED && (k.ROLE_CODE == roleDto.ROLE_CODE || k.ROLE_NAME == roleDto.ROLE_NAME));
                if (item != null && item.ROLE_CODE == roleDto.ROLE_CODE)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新角色失败,已经存在编码为:{roleDto.ROLE_CODE}的角色~" };
                else if (item != null && item.ROLE_CODE == roleDto.ROLE_NAME)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新角色失败,已经存在名称为:{roleDto.ROLE_NAME}的角色~" };
                else
                {
                    roleEntity.ROLE_NAME = roleDto.ROLE_NAME;
                    roleEntity.ROLE_DESCRIPTION = roleDto.ROLE_DESCRIPTION;
                    roleEntity.ROLE_INDEX = roleDto.ROLE_INDEX;
                    roleEntity.OPERATOR_ID = roleDto.OPERATOR_ID.Value;
                    roleEntity.UPDATE_TIME = roleDto.UPDATE_TIME.Value;
                    base.Repository.Update(roleEntity);
                    return new OperationResult { ResultType = OperationResultType.Succed, Message = "更新角色信息成功~" };
                }
            }
        }

        public OperationResult DeleteRole(RoleDto roleDto)
        {
            var roleEntity = base.Repository.GetById(roleDto.ID);
            if (roleEntity == null || roleEntity.IS_DELETED)
                return new OperationResult
                {
                    ResultType = OperationResultType.Failed,
                    Message = $"删除角色失败,无相应的角色~"
                };
            else
            {
                roleEntity.OPERATOR_ID = roleDto.OPERATOR_ID.Value;
                roleEntity.UPDATE_TIME = DateTime.Now;
                roleEntity.IS_DELETED = true;
                base.Repository.Update(roleEntity);
                return new OperationResult { ResultType = OperationResultType.Succed, Message = "删除角色成功~" };
            }
        }

        public List<SimplifiedRoleInfo> GetAllRoles()
        {
            var roles = (from s in base.Repository.GetByWhere(k => !k.IS_DELETED)
                         select new SimplifiedRoleInfo()
                         {
                             RoleId = s.ID,
                             RoleCode = s.ROLE_CODE,
                             RoleName = s.ROLE_NAME
                         }).ToList();

            return roles;
        }
    }
}
