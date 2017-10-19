using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Repository;
using ETPMS.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public class UserService : ETPMSBaseService<UM_USERINFO>, IUserService
    {
        public UserService(IRepository<UM_USERINFO> repository) : base(repository)
        {
        }

        public List<UserDto> GetUsers(UserStatus userStatus = UserStatus.IsActived)
        {
            var predicate = PredicateBuilder.True<UM_USERINFO>().And(s => !s.IS_DELETED);
            if (userStatus != UserStatus.UnCertain) { predicate = predicate.And(k => k.USER_STATUS == (int)userStatus); }
            var userItems = base.Repository.GetByWhere(predicate).MapToList<UM_USERINFO, UserDto>();

            return userItems;
        }
        public PagedList<UserDto> GetUsers(DateTime dateFrom, DateTime dateTo, int departmentId, PageDescriptor pageDescriptor)
        {
            var totalCount = 0;
            var predicate = PredicateBuilder.True<UM_USERINFO>()
                .And(s => s.CREATE_TIME >= dateFrom && s.CREATE_TIME <= dateTo && !s.IS_DELETED);
            if (departmentId > 0) { predicate = predicate.And(s => s.DEPARTMENT_ID == departmentId); }
            var items = base.Repository.GetPaged(predicate, out totalCount,
                        pageDescriptor.PageIndex, pageDescriptor.PageSize, pageDescriptor.SortField, pageDescriptor.IsAscending)
                        .MapToList<UM_USERINFO, UserDto>();

            return new PagedList<UserDto>(pageDescriptor.PageIndex, pageDescriptor.PageSize)
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public OperationResult AddUser(UserDto userDto)
        {
            if (base.Repository.GetByWhere(k => k.USER_CODE == userDto.USER_CODE && !k.IS_DELETED).Any())
                return new OperationResult
                {
                    ResultType = OperationResultType.ValidError,
                    Message = $"添加用户失败,已经存在帐号为:{userDto.USER_CODE}的用户~"
                };
            else
            {
                var userEntity = userDto.MapTo<UM_USERINFO>();
                userEntity.IS_DELETED = false;
                userEntity.PASSWORD = DESEncryptWrapper.Encrypt(ETPMSSetting.PassWord);
                base.Repository.Add(userEntity);
                return new OperationResult
                {
                    ResultType = OperationResultType.Succed,
                    Message = $"添加用户成功~"
                };
            }
        }

        public OperationResult UpdateUser(UserDto userDto)
        {
            var userEntity = base.Repository.GetById(userDto.ID);
            if (userEntity == null || userEntity.IS_DELETED)
                return new OperationResult { ResultType = OperationResultType.Failed, Message = $"更新用户信息失败,无相应的用户~" };
            else
            {
                var item = base.Repository.GetFirstOrDefualt(k => k.ID != userDto.ID && !k.IS_DELETED && (k.USER_CODE == userDto.USER_CODE || k.USER_NAME == userDto.USER_NAME));
                if (item != null && item.USER_CODE == userDto.USER_CODE)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新用户信息失败,已经存在帐号为:{userDto.USER_CODE}的用户~" };
                else if (item != null && item.USER_NAME == userDto.USER_NAME)
                    return new OperationResult { ResultType = OperationResultType.ValidError, Message = $"更新用户信息失败,已经存在姓名为:{userDto.USER_NAME}的用户~" };
                else
                {
                    userEntity.USER_NAME = userDto.USER_NAME;
                    userEntity.SEX = userDto.SEX;
                    userEntity.TELEPHONE = userDto.TELEPHONE;
                    userEntity.EMAIL = userDto.EMAIL;
                    userEntity.DEPARTMENT_ID = userDto.DEPARTMENT_ID;
                    userEntity.USER_STATUS = userDto.USER_STATUS;
                    userEntity.UPDATE_TIME = userDto.UPDATE_TIME.Value;
                    userEntity.OPERATOR_ID = userDto.OPERATOR_ID.Value;
                    base.Repository.Update(userEntity);
                    return new OperationResult { ResultType = OperationResultType.Succed, Message = "更新用户信息成功~" };
                }
            }
        }

        public OperationResult DeleteUser(UserDto userDto)
        {
            var userEntity = base.Repository.GetById(userDto.ID);
            if (userEntity == null || userEntity.IS_DELETED)
                return new OperationResult
                {
                    ResultType = OperationResultType.Failed,
                    Message = $"删除用户失败,无相应的用户~"
                };
            else
            {
                userEntity.OPERATOR_ID = userDto.OPERATOR_ID.Value;
                userEntity.UPDATE_TIME = DateTime.Now;
                userEntity.IS_DELETED = true;
                base.Repository.Update(userEntity);
                return new OperationResult { ResultType = OperationResultType.Succed, Message = "删除用户成功~" };
            }
        }

        public OperationResult ResetPassword(UserDto userDto)
        {
            var userEntity = base.Repository.GetById(userDto.ID);
            if (userEntity == null || userEntity.IS_DELETED)
                return new OperationResult
                {
                    ResultType = OperationResultType.Failed,
                    Message = $"重置用户密码失败,无相应的用户~"
                };
            else
            {
                userEntity.OPERATOR_ID = userDto.OPERATOR_ID.Value;
                userEntity.UPDATE_TIME = userDto.UPDATE_TIME.Value;
                userEntity.PASSWORD = DESEncryptWrapper.Encrypt(ETPMSSetting.PassWord);
                base.Repository.Update(userEntity);
                return new OperationResult { ResultType = OperationResultType.Succed, Message = $"用户密码已重置为系统默认密码:{ETPMSSetting.PassWord},请通知用户及时修改密码~" };
            }
        }

        public UserValidateResultType ValidateUser(string userCode)
        {
            var whereLambda = PredicateBuilder.True<UM_USERINFO>();
            whereLambda = whereLambda.And(p => p.USER_CODE.Equals(userCode.Trim()));

            var userEntity = base.Repository.GetFirstOrDefualt(whereLambda);
            if (userEntity == null)
                return UserValidateResultType.NotExist;
            else if (userEntity.IS_DELETED)
                return UserValidateResultType.Deleted;
            else if (userEntity.USER_STATUS == (byte)UserStatus.UnActived || userEntity.USER_STATUS == (byte)UserStatus.UnActived)
                return UserValidateResultType.NotActive;
            else if (userEntity.USER_STATUS == (byte)UserStatus.IsLocked)
                return UserValidateResultType.Locked;
            else//更新最后一次登录时间
            {
                userEntity.LAST_LOGIN_TIME = DateTime.Now;
                base.Repository.Update(userEntity);
                return UserValidateResultType.Successful;
            }
        }

        public UserValidateResultType ValidateUser(string userCode, string passWord, PasswordFormatType passwordFormat)
        {
            var whereLambda = PredicateBuilder.True<UM_USERINFO>();
            whereLambda = whereLambda.And(p => p.USER_CODE.Equals(userCode.Trim()));

            var userEntity = base.Repository.GetFirstOrDefualt(whereLambda);
            if (userEntity == null)
                return UserValidateResultType.NotExist;
            else if (userEntity.IS_DELETED)
                return UserValidateResultType.Deleted;
            else if (userEntity.USER_STATUS == (byte)UserStatus.UnActived || userEntity.USER_STATUS == (byte)UserStatus.UnActived)
                return UserValidateResultType.NotActive;
            else if (userEntity.USER_STATUS == (byte)UserStatus.IsLocked)
                return UserValidateResultType.Locked;
            else if (passwordFormat != PasswordFormatType.DESEncrypted)
                return UserValidateResultType.UnAvailablePasswordFormate;
            else if (!userEntity.PASSWORD.Equals(DESEncryptWrapper.Encrypt(passWord)))
                return UserValidateResultType.WrongPassword;
            else//更新最后一次登录时间
            {
                userEntity.LAST_LOGIN_TIME = DateTime.Now;
                base.Repository.Update(userEntity);
                return UserValidateResultType.Successful;
            }
        }

        public PasswordResetResultType ChangePassword(PasswordChangeDto passwordChangeDto, PasswordFormatType passwordFormat)
        {
            var whereLambda = PredicateBuilder.True<UM_USERINFO>();
            whereLambda = whereLambda.And(p => p.USER_CODE.Equals(passwordChangeDto.UserCode.Trim()));

            var userEntity = base.Repository.GetFirstOrDefualt(whereLambda);
            if (userEntity == null)
                return PasswordResetResultType.NotExist;
            else if (userEntity.IS_DELETED)
                return PasswordResetResultType.Deleted;
            else if (passwordFormat != PasswordFormatType.DESEncrypted)
                return PasswordResetResultType.UnAvailablePasswordFormate;
            else if (!userEntity.PASSWORD.Equals(DESEncryptWrapper.Encrypt(passwordChangeDto.OriginalPassword)))
                return PasswordResetResultType.WrongPassword;
            else if (passwordChangeDto.NewPassword.Length < 6)
                return PasswordResetResultType.WeakPassword;
            else if (!passwordChangeDto.NewPassword.Equals(passwordChangeDto.ConfirmPassword))
                return PasswordResetResultType.UnmatchedPassword;
            else//更新密码
            {
                userEntity.LAST_LOGIN_TIME = DateTime.Now;
                userEntity.PASSWORD = DESEncryptWrapper.Encrypt(passwordChangeDto.NewPassword);
                base.Repository.Update(userEntity);
                return PasswordResetResultType.Successful;
            }
        }
    }
}
