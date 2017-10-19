using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.Attributes;
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
    public sealed class DepartmentController : ETPMSBaseController
    {
        private IDepartmentService _departmentService;
        private IUserService _userService;

        public DepartmentController(IDepartmentService departmentService, IUserService userService)
        {
            this._departmentService = departmentService;
            this._userService = userService;
        }

        public ViewResult Departments()
        {
            return View();
        }

        [HttpGet]
        public JsonNetResult GetDepartments(DateTime dateFrom, DateTime dateTo)
        {
            Ensure.NotNull(base.PageDescriptor, "PageDescriptor");
            var pagedList = this._departmentService.GetDepartments(dateFrom, dateTo, base.PageDescriptor);

            return new JsonNetResult(pagedList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonNetResult GetDepartmentTree()
        {
            var departmentTree = this._departmentService.GetDepartmentTree(); ;
            return new JsonNetResult(new List<DepartmentTreeDescriptor> { departmentTree }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonNetResult GetChildDepartmentNode(int Id = 0)
        {
            Ensure.NonNegative(Id, "部门Id");
            var childDepartmentNodes = this._departmentService.GetChildDepartmentNodeByDepartmentId(Id);
            return new JsonNetResult(childDepartmentNodes, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DepartmentEdit(int departmentId, string parentDepartmentName)
        {
            Ensure.NonNegative(departmentId, "部门Id");
            parentDepartmentName = string.IsNullOrWhiteSpace(parentDepartmentName) ? "华北电力研究院" : parentDepartmentName;
            var departmentDto = this._departmentService.GetDepartmentById(departmentId);

            var userItems = from s in this._userService.GetUsers(UserStatus.IsActived)
                            select new ValueTextPair<int, string>(s.ID.Value, s.USER_NAME);

            this.ViewBag.ParentDepartmentName = parentDepartmentName;
            this.ViewBag.AllUsers = Serializer.Serialize(userItems);
            return PartialView(departmentDto);
        }

        public PartialViewResult DepartmentAdd(int parentDepartmentId = 0, string parentDepartmentName = "华北电力研究院")
        {
            Ensure.NonNegative(parentDepartmentId, "上级部门Id");
            parentDepartmentName = string.IsNullOrWhiteSpace(parentDepartmentName) ? "华北电力研究院" : parentDepartmentName;
            var departmentDto = new DepartmentDto { FATHER_DEPARTMENT_ID = parentDepartmentId };

            var userItems = from s in this._userService.GetUsers(UserStatus.IsActived)
                            select new ValueTextPair<int, string>(s.ID.Value, s.USER_NAME);

            this.ViewBag.ParentDepartmentName = parentDepartmentName;
            this.ViewBag.AllUsers = Serializer.Serialize(userItems);
            return PartialView(departmentDto);
        }

        [HttpPost]
        public JsonNetResult AddDepartment(DepartmentDto departmentDto)
        {
            Ensure.NotNull(departmentDto, "部门信息");
            var validationResult = new DepartmentValidator().Validate(departmentDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = Enums.ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                departmentDto.CREATE_TIME = DateTime.Now;
                departmentDto.UPDATE_TIME = ETPMSSetting.MinTime;
                departmentDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._departmentService.AddDepartment(departmentDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost]
        public JsonNetResult UpdateDepartment(DepartmentDto departmentDto)
        {
            Ensure.NotNull(departmentDto, "部门信息");
            Ensure.Meet(k => k.ID >= 0, departmentDto, "部门Id不能小于等于0~");
            var validationResult = new DepartmentValidator().Validate(departmentDto);
            if (!validationResult.IsValid)
                return new JsonNetResult(new ResponseModel
                {
                    ResultType = Enums.ResponseResultType.Info,
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~"
                });
            else
            {
                departmentDto.UPDATE_TIME = DateTime.Now;
                departmentDto.OPERATOR_ID = base.CurrentUser.UserId;
                var operationResult = this._departmentService.UpdateDepartment(departmentDto);
                return new JsonNetResult(operationResult.MapToResponseModel());
            }
        }

        [HttpPost]
        public JsonNetResult DeleteDepartment(int departmentId)
        {
            Ensure.Positive(departmentId, "部门Id");
            var departmentDto = new DepartmentDto
            {
                ID = departmentId,
                UPDATE_TIME = DateTime.Now,
                OPERATOR_ID = base.CurrentUser.UserId
            };
            var operationResult = this._departmentService.DeleteDepartment(departmentDto);
            return new JsonNetResult(operationResult.MapToResponseModel());
        }
    }
}