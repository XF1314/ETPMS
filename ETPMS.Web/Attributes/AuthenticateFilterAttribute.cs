using ETPMS.Application.Implementations;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.Models;
using System;
using System.Web.Mvc;

namespace ETPMS.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthenticateFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AnonymousAttribute), false))//非匿名登录
            {
                var formsAuthenticationService = ObjectContainer.Current.Resolve<FormsAuthenticationService>();
                if (formsAuthenticationService.IsAuthenticated())//进行登录认证
                {
                    var workContext = this.GetWorkContextBySession();
                    if (workContext == null || this.GetWorkContextBySession().IsAnonymous)
                    {
                        var currentUser = formsAuthenticationService.GetAuthenticatedUser();
                        if (currentUser != null)
                        {
                            SessionHelper.SetSession(ETPMSSetting.G_WorkContextSessionName, new ETPMSContext(false) { CurrentUser = currentUser });
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult(ETPMSSetting.G_LoginUrl);
                        }
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult(ETPMSSetting.G_LoginUrl);
                }
            }
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