using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Enums;
using ETPMS.Application.Implementations;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.Attributes;
using ETPMS.Web.Enums;
using ETPMS.Web.Extensions;
using ETPMS.Web.Models;
using ETPMS.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ETPMS.Web.Controllers
{
    public sealed class UserController : ETPMSBaseController
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserRoleService _userRoleService;
        //private IDepartmentService _departmentService;

        public UserController(IUserService userService, IRoleService roleService, IUserRoleService userRoleService)
        {
            this._userService = userService;
            this._roleService = roleService;
            this._userRoleService = userRoleService;
        }

        #region 用户
        [AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public ViewResult Users()
        {
            var departmentService = ServiceContainer.Resolve<IDepartmentService>();
            var allDepartments = (from s in departmentService.GetAllDepartments()
                                  select new ValueTextPair<int, string>(s.DepartmentId, s.DepartmentName))
                                 .ToList();

            //allDepartments.Add(new ValueTextPair<int, string>(0, "请选择"));
            ViewBag.AllDepartments = Serializer.Serialize(allDepartments);
            ViewBag.AllSexes = Serializer.Serialize(typeof(Sex).ToIntValueTextList());
            ViewBag.AllUserStatus = Serializer.Serialize(typeof(UserStatus).ToIntValueTextList());
            ViewBag.DateFrom = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
            ViewBag.DateTo = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

            return View();
        }

        [HttpGet]
        [AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult GetUsers(DateTime dateFrom, DateTime dateTo, int departmentId)
        {
            Ensure.NonNegative(departmentId, "部门Id");
            Ensure.NotNull(base.PageDescriptor, "PageDescriptor");
            var pagedList = this._userService.GetUsers(dateFrom, dateTo, departmentId, base.PageDescriptor);

            return new JsonNetResult(pagedList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult AddUser([ModelBinder(typeof(JsonBinder))] UserDto userDto)
        {
            Ensure.NotNull(userDto, "用户信息");
            var validationResult = new UserValidator().Validate(userDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                userDto.CREATE_TIME = DateTime.Now;
                userDto.UPDATE_TIME = ETPMSSetting.MinTime;
                userDto.LAST_LOGIN_TIME = ETPMSSetting.MinTime;
                userDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._userService.AddUser(userDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult UpdateUser([ModelBinder(typeof(JsonBinder))] UserDto userDto)
        {
            Ensure.NotNull(userDto, "用户信息");
            Ensure.Meet<UserDto>(k => k.ID >= 0, userDto, "用户Id不能小于等于0~");
            var validationResult = new UserValidator().Validate(userDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = Enums.ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                userDto.UPDATE_TIME = DateTime.Now;
                userDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._userService.UpdateUser(userDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult DeleteUser([ModelBinder(typeof(JsonBinder))] UserDto userDto)
        {
            Ensure.NotNull(userDto, "用户信息");
            Ensure.Meet<UserDto>(k => k.ID >= 0, userDto, "用户Id不能小于等于0~");

            userDto.UPDATE_TIME = DateTime.Now;
            userDto.OPERATOR_ID = base.CurrentUser.UserId;
            var operationResult = this._userService.DeleteUser(userDto);
            return new JsonNetResult(operationResult.MapToResponseModel());
        }

        [HttpPost, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult ResetPassword([ModelBinder(typeof(JsonBinder))] UserDto userDto)
        {
            Ensure.NotNull(userDto, "用户信息");
            Ensure.Meet<UserDto>(k => k.ID >= 0, userDto, "用户Id不能小于等于0~");

            userDto.UPDATE_TIME = DateTime.Now;
            userDto.OPERATOR_ID = base.CurrentUser.UserId;
            var operationResult = this._userService.ResetPassword(userDto);
            return new JsonNetResult(operationResult.MapToResponseModel());
        }


        public ViewResult PasswordChange()
        {
            var passwordChangeDto = new PasswordChangeDto { UserCode = base.CurrentUser.UserCode };
            return View(passwordChangeDto);
        }

        [HttpPost]
        public JsonNetResult ChangePassword(PasswordChangeDto passwordChangeDto)
        {
            Ensure.NotNull(passwordChangeDto, "密码信息");
            var validationResult = new PasswordChangeValidator().Validate(passwordChangeDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                var passwordResetResult = this._userService.ChangePassword(passwordChangeDto, PasswordFormatType.DESEncrypted);
                var responseModel = new ResponseModel
                {
                    ResultType = passwordResetResult == PasswordResetResultType.Successful ? ResponseResultType.Succed : ResponseResultType.Info,
                    Message = passwordResetResult.GetDescription()
                };

                if (responseModel.ResultType == ResponseResultType.Succed) { base.ServiceContainer.Resolve<FormsAuthenticationService>().SignOut(); };
                return new JsonNetResult(responseModel);
            }
        }
        #endregion

        #region 用户角色

        [HttpGet, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult GetUserRoles(int userId)
        {
            var roleIds = from s in this._userRoleService.GetRolesByUserId(userId)
                          select s.RoleId;
            return new JsonNetResult(new ResponseModel { Data = roleIds, ResultType = ResponseResultType.Succed }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost, AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
        public JsonNetResult UpdateUserRole(int userId, string roleIds)
        {
            Ensure.NonNegative(userId, "用户Id");
            Ensure.NotNullOrEmpty(roleIds, "角色Ids");
            var userRoleDtos = new List<UserRoleDto>();
            var roleIdList = Array.ConvertAll<string, int>(roleIds.Split(new char[] { ETPMSSetting.Spliter }), s => int.Parse(s));
            roleIdList.ForEach(s => userRoleDtos.Add(new UserRoleDto
            {
                ROLE_ID = s,
                USER_ID = userId,
                CREATER_ID = base.CurrentUser.UserId,
                CREATE_TIME = DateTime.Now
            }));

            var responseModel = this._userRoleService.UpdateUserRole(userId, userRoleDtos).MapToResponseModel();
            return new JsonNetResult(responseModel);
        }

        #endregion

    }
}