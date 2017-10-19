using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Infrastructure.Configurations;
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
    [AuthorizeFilter(Roles = "SuperAdmin|SystemAdmin")]
    public sealed class RoleController : ETPMSBaseController
    {
        private IRoleService _roleService;
        private IRoleMenuService _roleMenuService;

        public RoleController(IRoleService roleService, IRoleMenuService roleMenuService)
        {
            this._roleService = roleService;
            this._roleMenuService = roleMenuService;
        }

        #region 角色
        public ViewResult Roles()
        {
            ViewBag.DateFrom = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
            ViewBag.DateTo = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            return View();
        }

        [HttpGet]
        public JsonNetResult GetRoles(DateTime dateFrom, DateTime dateTo)
        {
            Ensure.NotNull(base.PageDescriptor, "PageDescriptor");
            var pagedList = this._roleService.GetRoles(dateFrom, dateTo, base.PageDescriptor);

            return new JsonNetResult(pagedList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult AddRole([ModelBinder(typeof(JsonBinder))]RoleDto roleDto)
        {
            Ensure.NotNull(roleDto, "角色信息");
            var validationResult = new RoleValidator().Validate(roleDto);

            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = Enums.ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                roleDto.CREATE_TIME = DateTime.Now;
                roleDto.UPDATE_TIME = ETPMSSetting.MinTime;
                roleDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._roleService.AddRole(roleDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost]
        public JsonNetResult UpdateRole([ModelBinder(typeof(JsonBinder))]RoleDto roleDto)
        {
            Ensure.NotNull(roleDto, "角色信息");
            Ensure.Meet(k => k.ID >= 0, roleDto, "角色Id不能小于等于0~");

            var validationResult = new RoleValidator().Validate(roleDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = Enums.ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                roleDto.UPDATE_TIME = DateTime.Now;
                roleDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._roleService.UpdateRole(roleDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost]
        public JsonNetResult DeleteRole([ModelBinder(typeof(JsonBinder))]RoleDto roleDto)
        {
            Ensure.NotNull(roleDto, "角色信息");
            Ensure.Meet(k => k.ID >= 0, roleDto, "角色Id不能小于等于0~");

            roleDto.UPDATE_TIME = DateTime.Now;
            roleDto.OPERATOR_ID = base.CurrentUser.UserId;
            var operationResult = this._roleService.DeleteRole(roleDto);
            return new JsonNetResult(operationResult.MapToResponseModel());
        }

        [HttpGet]
        public JsonNetResult GetAllRoles()
        {
            var roles = this._roleService.GetAllRoles();
            return new JsonNetResult(new ResponseModel { Data = roles, ResultType = ResponseResultType.Succed }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 角色菜单
        [HttpGet]
        public JsonNetResult GetMenuTree(int roleId)
        {
            Ensure.NonNegative(roleId, "角色Id");
            var menuTree = this._roleMenuService.GetRoleMenu(roleId);

            return new JsonNetResult(new List<RoleMenuTreeDescriptor> { menuTree }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult UpdateRoleMenu(int roleId, string menuIds)
        {
            Ensure.NonNegative(roleId, "角色Id");
            Ensure.NotNullOrEmpty(menuIds, "菜单Ids");
            var roleMenuDtos = new List<RoleMenuDto>();
            Array.ConvertAll(menuIds.Split(new char[] { ETPMSSetting.Spliter }), s => int.Parse(s)).ToList().ForEach(s =>
            {
                if (s > 0)
                    roleMenuDtos.Add(new RoleMenuDto
                    {
                        MENU_ID = s,
                        ROLE_ID = roleId,
                        CREATER_ID = base.CurrentUser.UserId,
                        CREATE_TIME = DateTime.Now
                    });
            });

            var operationResult = this._roleMenuService.UpdateRoleMenu(roleId, roleMenuDtos);
            return new JsonNetResult(operationResult.MapToResponseModel());
        }
        #endregion
    }
}