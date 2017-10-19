using ETPMS.Application.Contracts;
using ETPMS.Web.Extensions;
using System.Web.Mvc;

namespace ETPMS.Web.Controllers
{
    public sealed class MenuController : ETPMSBaseController
    {
        private readonly IMenuService _menuService;
        private readonly IRoleMenuService _roleMenuService;


        public MenuController(IMenuService menuService, IRoleMenuService roleMenuService)
        {
            this._menuService = menuService;
            this._roleMenuService = roleMenuService;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAuthorizedMenus()
        {
            var menuDescriptors = this._roleMenuService.GetAllMenus();
            return Serializer.Serialize(menuDescriptors);
        }
    }
}