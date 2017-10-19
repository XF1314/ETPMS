using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Entity;
using System.Collections.Generic;

namespace ETPMS.Application.Contracts
{
    public interface IRoleMenuService 
    {
        /// <summary>
        /// 获取特定角色的菜单权限描述
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        RoleMenuTreeDescriptor GetRoleMenu(int roleId);

        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        List<MenuTreeDescriptor> GetAllMenus();

        /// <summary>
        /// 获取特定角色具有访问权限的菜单
        /// </summary>
        /// <param name="roleIds">角色Ids</param>
        /// <returns></returns>
        List<MenuTreeDescriptor> GetMenuByRoles(IList<string> roleCodes);

        /// <summary>
        /// 获取特定用户具有访问权限的菜单
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        List<MenuTreeDescriptor> GetMenuByUser(int userId);

        /// <summary>
        /// 更新角色菜单权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleMenuDtos">角色菜单Dtos</param>
        /// <returns></returns>
        OperationResult UpdateRoleMenu(int roleId, IList<RoleMenuDto> roleMenuDtos);

    }
}
