using ETPMS.Web.Attributes;
using ETPMS.Web.Extensions;
using System.Web.Mvc;

namespace ETPMS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthenticateFilterAttribute());
            //filters.Add(new HandleExceptionAttribute());
            filters.Add(new JsonHandlerAttribute());
            filters.Add(new XSSFilterAttribute());
        }
    }
}
