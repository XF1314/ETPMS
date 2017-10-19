using ETPMS.Application.DTOs;
using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Entity;
using System;
using System.Collections.Generic;

namespace ETPMS.Application.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// 获取特定状态用户
        /// </summary>
        /// <param name="userStatus">用户状态</param>
        /// <returns></returns>
        List<UserDto> GetUsers(UserStatus userStatus);

        /// <summary>
        /// 用户分页查询
        /// </summary>
        /// <param name="dateFrom">创建日期(开始)</param>
        /// <param name="dateTo">创建日期(结束)</param>
        /// <param name="departmentId">所属部门Id</param>
        /// <param name="pageDescriptor">分页条件</param>
        /// <returns></returns>
        PagedList<UserDto> GetUsers(DateTime dateFrom, DateTime dateTo, int departmentId, PageDescriptor pageDescriptor);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto">userDto</param>
        /// <returns></returns>
        OperationResult AddUser(UserDto userDto);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userDto">userDto</param>
        /// <returns></returns>
        OperationResult UpdateUser(UserDto userDto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userDto">userDto</param>
        /// <returns></returns>
        OperationResult DeleteUser(UserDto userDto);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="userDto">usrDto</param>
        /// <returns></returns>
        OperationResult ResetPassword(UserDto userDto);

        /// <summary>
        /// 用户有效性验证
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        UserValidateResultType ValidateUser(string userCode);

        /// <summary>
        /// 用户有效性验证
        /// </summary>
        /// <param name="userCode">用户编码</param>
        /// <param name="passWord">密码</param>
        /// <param name="passwordFormat">密码格式</param>
        /// <returns></returns>
        UserValidateResultType ValidateUser(string userCode, string passWord, PasswordFormatType passwordFormat);

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="passwordChangeDto">密码信息</param>
        /// <param name="passwordFormat">密码格式</param>
        /// <returns></returns>
        PasswordResetResultType ChangePassword(PasswordChangeDto passwordChangeDto, PasswordFormatType passwordFormat);
    }
}
