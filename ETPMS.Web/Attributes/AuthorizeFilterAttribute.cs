using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Logging;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETPMS.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            var roles = Roles.Split(new char[] { ETPMSSetting.Spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var users = Users.Split(new char[] { ETPMSSetting.Spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var workContext = this.GetWorkContextBySession();
            if (workContext?.CurrentUser != null)
            {
                if (!users.Any() && !roles.Any())//如果为空则说明不需要对Action进行权限控制
                {
                    isAuthorized = true;
                }
                else if (users.Any() && users.Contains(workContext.CurrentUser.UserCode))
                {
                    isAuthorized = true;
                }
                else if (roles.Any() && roles.Intersect(workContext.CurrentUser.UserRoles).Any())
                {
                    isAuthorized = true;
                }
            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var workContext = this.GetWorkContextBySession();
            var logger = ObjectContainer.Current.Resolve<ILoggerFactory>().Create("UnauthorizeRequest");
            logger.WarnFormat("用户{0}于{1}尝试未授权访问{2}/{3}功能", workContext.CurrentUser.UserName, DateTime.Now.ToLongTimeString(), filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            filterContext.HttpContext.Response.Redirect("~/Home/UnAuthorizedException");
        }

        private ETPMSContext GetWorkContextBySession()
        {
            ETPMSContext workContext = null;
            if (SessionHelper.GetSession(ETPMSSetting.G_WorkContextSessionName) != null)
            {
                workContext = SessionHelper.GetSession(ETPMSSetting.G_WorkContextSessionName) as ETPMSContext;
            }

            return workContext;
        }
    }
}