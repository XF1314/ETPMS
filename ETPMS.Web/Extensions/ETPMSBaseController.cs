using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Logging;
using ETPMS.Infrastructure.Serializing;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ETPMS.Web.Extensions
{
    public class ETPMSBaseController : Controller
    {
        public static string SystemCode { private set; get; } = ETPMSSetting.G_SystemName;
        public static string SystemName { private set; get; } = ETPMSSetting.G_SystemCode;

        public PageDescriptor PageDescriptor { get; private set; }
        public SimplifiedUserInfo CurrentUser => WorkContext?.CurrentUser;
        public ETPMSContext WorkContext => SessionHelper.GetSession(ETPMSSetting.G_WorkContextSessionName) as Web.Models.ETPMSContext;

        public ILogger Logger { get; private set; }
        public IJsonSerializer Serializer { get; private set; }
        public IObjectContainer ServiceContainer { get; } = ObjectContainer.Current;

        public ETPMSBaseController()
        {
            this.Logger = ObjectContainer.Current.Resolve<ILoggerFactory>().Create(GetType().FullName);
            this.Serializer = ObjectContainer.Current.Resolve<IJsonSerializer>();
            var workContext = this.WorkContext ?? new ETPMSContext(true)
            {
                ObjectContainer = ObjectContainer.Current,
                CurrentUser = new SimplifiedUserInfo
                {
                    UserCode = new Guid().ToString(),
                    UserName = "匿名",
                    PhoneNumber = string.Empty,
                    EmailAddress = string.Empty,
                    Sex = Sex.Unkown,
                    DepartmentId = 0,
                    UserRoles = new List<string>()
                }
            };

            SessionHelper.SetSession(ETPMSSetting.G_WorkContextSessionName, workContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request["pageSize"] != null && request["pageIndex"] != null)
            {
                this.PageDescriptor = new PageDescriptor()
                {
                    PageSize = int.Parse(request["pageSize"]),
                    PageIndex = int.Parse(request["pageIndex"]),
                    IsAscending = request["sortDir"] == "asc" ? true : false,
                    SortField = request["sortField"] ?? "ID"
                };
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            this.Logger.Error(exception.Message, exception);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var message = "请求异常：";
                if (exception is HttpAntiForgeryException)
                {
                    message += "安全性验证失败。<br>请刷新页面重试，详情请查看系统日志。";
                }
                else
                {
                    message += exception.Message;
                }

                this.Logger.Error(message);
                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonNetResult(new ResponseModel { Message = message, ResultType = Enums.ResponseResultType.Error });
            }
        }
    }
}