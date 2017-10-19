using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ETPMS.Web.Attributes
{
    public class XSSFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParams = filterContext.ActionParameters;
            if (actionParams != null && actionParams.Any())
            {
                IDictionary<string, object> newParams = new Dictionary<string, object>();
                this.GetNewParam(filterContext, actionParams, newParams);
                this.RebuildOldParam(newParams, actionParams);
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 把防xss过滤后的参数放回到参数字典中
        /// </summary>
        /// <param name="newParams">新参数字典</param>
        /// <param name="actionParams">方法参数字典</param>
        private void RebuildOldParam(IDictionary<string, object> newParams, IDictionary<string, object> actionParams)
        {
            if (newParams.Count > 0)
            {
                actionParams.Clear();
                foreach (var param in newParams)
                {
                    actionParams.Add(param);
                }
            }
        }

        /// <summary>
        /// 过滤xss攻击得到新参数字典
        /// </summary>
        /// <param name="filterContext">方法上下文对象</param>
        /// <param name="actionParams">方法参数字典</param>
        /// <param name="newParams">新参数字典</param>
        private void GetNewParam(ActionExecutingContext filterContext, IDictionary<string, object> actionParams, IDictionary<string, object> newParams)
        {
            foreach (var param in actionParams)
            {
                var paramValue = param.Value as string;
                newParams.Add(param.Key, param.Value);
                //待完善
                //newParams.Add(param.Key, !string.IsNullOrEmpty(paramValue) ? filterContext.HttpContext.Server.HtmlDecode(XssHelper.AntiXssInput(paramValue)) : param.Value);
            }
        }
    }
}