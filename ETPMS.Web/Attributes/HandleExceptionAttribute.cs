using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Logging;
using ETPMS.Web.Extensions;
using System;
using System.Net;
using System.Web.Mvc;

namespace ETPMS.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class HandleExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var errorMessage = GetErrorMessage(filterContext);
            ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().Name).Error(errorMessage, filterContext.Exception);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        errorMsg = errorMessage
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                if (filterContext.Exception != null && filterContext.Exception is TimeoutException)
                {
                    View = "TimeoutError";
                }
                base.OnException(filterContext);
            }
        }

        private static string GetErrorMessage(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                if (filterContext.Exception is TimeoutException)
                {
                    return "服务器处理请求超时";
                }
                return filterContext.Exception.Message;
            }
            return "服务器未知错误";
        }
    }
}