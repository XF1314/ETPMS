using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Entity;
using System;
using System.Collections.Generic;

namespace ETPMS.Application.Contracts
{
    public interface IRoleService 
    {
        /// <summary>
        /// 角色分页查询
        /// </summary>
        /// <param name="dateFrom">创建日期(开始)</param>
        /// <param name="dateTo">创建日期(结束)</param>
        /// <param name="pageDescriptor">分页条件</param>
        /// <returns></returns>
        PagedList<RoleDto> GetRoles(DateTime dateFrom, DateTime dateTo, PageDescriptor pageDescriptor);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleDto">roleDto</param>
        /// <returns></returns>
        OperationResult AddRole(RoleDto roleDto);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        OperationResult UpdateRole(RoleDto roleDto);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleDto">删除角色</param>
        /// <returns></returns>
        OperationResult DeleteRole(RoleDto roleDto);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        List<SimplifiedRoleInfo> GetAllRoles();

    }
}
