using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Application.Models;
using ETPMS.Entity;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerLifetimeScope)]
    public sealed class RoleMenuService : ETPMSBaseService<UM_ROLE_RELMENU>, IRoleMenuService
    {
        private readonly IRepository<UM_ROLE> _roleRepository;
        private readonly IRepository<UM_MENU> _menuRepository;
        private readonly IUserRoleService _userRoleService;

        public RoleMenuService(IRepository<UM_ROLE_RELMENU> roleMenuRepository,
          IRepository<UM_ROLE> roleRepository, IRepository<UM_MENU> menuRepository, IUserRoleService userRoleService) : base(roleMenuRepository)
        {
            this._roleRepository = roleRepository;
            this._menuRepository = menuRepository;
            this._userRoleService = userRoleService;
        }

        public RoleMenuTreeDescriptor GetRoleMenu(int roleId)
        {
            var menuEntities = (from k in this._menuRepository.GetAll()
                                where !k.IS_DELETED && k.IS_VISIBLE
                                select k).ToList();
            var authorizedMenuIds = (from k in base.Repository.GetByWhere(k => k.ROLE_ID == roleId) select k.MENU_ID).ToList();
            var rootMenu = new RoleMenuTreeDescriptor { MenuID = 0, MenuCode = "ROOT", MenuName = "设备试验项目管理系统", ImgUrl = "pf-user-dpt-sprite", };

            var lv1MenuIds = (from k in menuEntities where k.FATHER_MENU_ID == 0 select k.ID).ToList();
            if(lv1MenuIds.Any())
            {
                rootMenu.Expanded = true;
                rootMenu.HasChildMenus = true;
                rootMenu.ChildMenus = new List<RoleMenuTreeDescriptor>();
                lv1MenuIds.ForEach(k => {
                    rootMenu.ChildMenus.Add(this.GetRoleMenu(k, menuEntities, authorizedMenuIds));
                });
            }

            return rootMenu;
        }

        public List<MenuTreeDescriptor> GetAllMenus()
        {
            var allMenuEntities = this._menuRepository.GetByWhere(k => k.IS_VISIBLE && !k.IS_DELETED).ToList();
            var menuDescriptors = (from k in allMenuEntities
                                   where k.FATHER_MENU_ID == 0//顶级菜单
                                   select this.GetMenuDescriptor(k.ID, allMenuEntities))
                                   .OrderBy(k => k.SortIndex).ToList();

            return menuDescriptors;
        }

        public List<MenuTreeDescriptor> GetMenuByUser(int userId)
        {
            var roleCodes = from s in this._userRoleService.GetRolesByUserId(userId)
                            select s.RoleCode;

            return this.GetMenuByRoles(roleCodes.ToList());
        }

        public List<MenuTreeDescriptor> GetMenuByRoles(IList<string> roleCodes)
        {
            var allMenuEntities = (from s in base.Repository.GetAll()
                                   join p in this._roleRepository.GetAll() on s.ROLE_ID equals p.ID
                                   join q in this._menuRepository.GetAll() on s.MENU_ID equals q.ID
                                   where roleCodes.Contains(p.ROLE_CODE) && !p.IS_DELETED && !q.IS_DELETED && q.IS_VISIBLE
                                   select q).ToList();

            var menuDescriptors = (from k in allMenuEntities
                                   where k.FATHER_MENU_ID == 0//顶级菜单
                                   select this.GetMenuDescriptor(k.ID, allMenuEntities))
                                   .OrderBy(k => k.SortIndex).ToList();

            return menuDescriptors;
        }

        public OperationResult UpdateRoleMenu(int roleId, IList<RoleMenuDto> roleMenuDtos)
        {
            //ToDo:先清空RoleId对应的菜单权限，再添加新的菜单权限
            this.Repository.Delete(s => s.ROLE_ID == roleId);
            var roleMenuEntities = roleMenuDtos.MapToList<RoleMenuDto, UM_ROLE_RELMENU>();
            this.Repository.Add(roleMenuEntities);

            return new OperationResult { ResultType = Enums.OperationResultType.Succed, Message = "更新角色菜单权限成功~" };
        }


        private MenuTreeDescriptor GetMenuDescriptor(int menuId, List<UM_MENU> allMenuEntities)
        {
            MenuTreeDescriptor menuDescriptor = null;
            var menuEntity = allMenuEntities.FirstOrDefault(p => p.ID == menuId);
            if (menuEntity != null)
            {
                menuDescriptor = new MenuTreeDescriptor
                {
                    MenuCode = menuEntity.MENU_CODE,
                    MenuName = menuEntity.MENU_NAME,
                    TransferUrl = menuEntity.TRANSFER_URL,
                    ImageUrl = menuEntity.IMG_URL,
                    CssClass = menuEntity.CSS_CLASS,
                    SortIndex = menuEntity.MENU_INDEX
                };

                var allChildMenus = allMenuEntities.FindAll(k => k.FATHER_MENU_ID == menuEntity.ID);
                if (allChildMenus.Any())
                {
                    var childMenuDescriptor = new List<MenuTreeDescriptor>();
                    allChildMenus.ForEach(k =>
                    {
                        childMenuDescriptor.Add(GetMenuDescriptor(k.ID, allChildMenus));
                    });
                    menuDescriptor.ChildMenus = childMenuDescriptor.OrderBy(k => k.SortIndex).ToList();
                }
            }

            return menuDescriptor;
        }

        private RoleMenuTreeDescriptor GetRoleMenu(int menuId, List<UM_MENU> allMenuEntities, List<int> authorizedMenuIds)
        {
            RoleMenuTreeDescriptor roleMenuDescriptor = null;
            var menuEntity = allMenuEntities.FirstOrDefault(p => p.ID == menuId);
            if (menuEntity != null)
            {
                roleMenuDescriptor = new RoleMenuTreeDescriptor
                {
                    MenuID = menuEntity.ID,
                    MenuCode = menuEntity.MENU_CODE,
                    MenuName = menuEntity.MENU_NAME,
                    Url = menuEntity.TRANSFER_URL,
                    Checked = authorizedMenuIds.Contains(menuEntity.ID),
                    ImgUrl = "pf-user-dpt-sprite", //menuEntity.EXT,
                    SortIndex = (byte)menuEntity.MENU_INDEX,
                    FatherMenuId = menuEntity.FATHER_MENU_ID,
                    Expanded = menuEntity.FATHER_MENU_ID == 0,
                    ChildMenus = new List<RoleMenuTreeDescriptor>(),
                    HasChildMenus = false
                };
                var allChildMenuEntities = allMenuEntities.FindAll(p => p.FATHER_MENU_ID == menuEntity.ID);
                if (allChildMenuEntities.Any())
                {
                    roleMenuDescriptor.HasChildMenus = true;
                    allChildMenuEntities.ForEach(k =>
                    {
                        roleMenuDescriptor.ChildMenus.Add(this.GetRoleMenu(k.ID, allMenuEntities, authorizedMenuIds));
                    });
                }
            }
            return roleMenuDescriptor;
        }
    }
}
