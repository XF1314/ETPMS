using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Entity;
using System.Collections.Generic;

namespace ETPMS.Application.Contracts
{
    public interface IUserRoleService 
    {
        /// <summary>
        /// 根据用户Id取角色信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        List<SimplifiedRoleInfo> GetRolesByUserId(int userId);

        /// <summary>
        /// 根据用户Code取角色信息
        /// </summary>
        /// <param name="UserCode">用户Code</param>
        /// <returns></returns>
        List<SimplifiedRoleInfo> GetRolesByUserCode(string UserCode);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userRoleDtos">用户角色Dto</param>
        /// <returns></returns>
        OperationResult UpdateUserRole(int userId, List<UserRoleDto> userRoleDtos);

    }
}
