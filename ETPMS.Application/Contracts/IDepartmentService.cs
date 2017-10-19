using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Entity;
using System;
using System.Collections.Generic;

namespace ETPMS.Application.Contracts
{
    public interface IDepartmentService
    {

        /// <summary>
        /// 获取所有部门简要信息
        /// </summary>
        /// <returns></returns>
        List<SimplifiedDepartmentInfo> GetAllDepartments();

        /// <summary>
        /// 部门分页查询
        /// </summary>
        /// <param name="dateFrom">创建日期(开始)</param>
        /// <param name="dateTo">创建日期(结束)</param>
        /// <param name="pageDescriptor">分页条件</param>
        /// <returns></returns>
        PagedList<DepartmentDto> GetDepartments(DateTime dateFrom, DateTime dateTo, PageDescriptor pageDescriptor);

        /// <summary>
        /// 获取部门树
        /// </summary>
        /// <returns></returns>
        DepartmentTreeDescriptor GetDepartmentTree();

        /// <summary>
        /// 根据部门Id获取子部门节点
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        List<DepartmentNodeDescriptor> GetChildDepartmentNodeByDepartmentId(int departmentId);

        /// <summary>
        /// 根据部门Id获取部门信息
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        DepartmentDto GetDepartmentById(int departmentId);

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="departmentDto">departmentDto</param>
        /// <returns></returns>
        OperationResult AddDepartment(DepartmentDto departmentDto);

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="departmentDto">departmentDto</param>
        /// <returns></returns>
        OperationResult UpdateDepartment(DepartmentDto departmentDto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentDto">departmentDto</param>
        /// <returns></returns>
        OperationResult DeleteDepartment(DepartmentDto departmentDto);

    }
}
