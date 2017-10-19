using System.Web.ApplicationServices;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System;
using ETPMS.Application.Contracts;
using ETPMS.Application.DTOs;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Application.Enums;
using ETPMS.Application.Models;
using ETPMS.Infrastructure.Repository;
using ETPMS.Entity;

namespace ETPMS.Application.Implementations
{
    [Component(LifeStyle.InstancePerDependency)]
    public sealed class FormsAuthenticationService : AuthenticationService
    {
        #region Fields
        private readonly HttpContext _httpContext;
        private readonly TimeSpan _expirationTimeSpan;
        private readonly IRepository<UM_USERINFO> _userRepository;
        private readonly IRepository<UM_ROLE> _roleRepository;
        private readonly IRepository<UM_USER_RELROLE> _userRoleRepository;
        #endregion

        #region Ctor
        public FormsAuthenticationService(IRepository<UM_USERINFO> userRepository, IRepository<UM_ROLE> roleRepository, IRepository<UM_USER_RELROLE> userRoleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._userRoleRepository = userRoleRepository;
            this._httpContext = HttpContext.Current;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }
        #endregion

        #region Methods
        public bool IsAuthenticated()
        {
            return this._httpContext?.Request != null
             && this._httpContext.Request.IsAuthenticated
             && this._httpContext.User.Identity is FormsIdentity;
        }

        public void SignIn(string userCode, bool createPersistentCookie)
        {
            var dateTimeNow = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(//初始化票据
                1 /*version*/,
                userCode,
                dateTimeNow,
                dateTimeNow.Add(this._expirationTimeSpan),
                createPersistentCookie,
                string.Join(ETPMSSetting.Spliter.ToString(), this.GetUserRoles(userCode).ToArray()),//用户所被分配的角色
                FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);//加密票据
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,//设置cookie不可更改
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };
            if (ticket.IsPersistent) { cookie.Expires = ticket.Expiration; }
            if (FormsAuthentication.CookieDomain != null) { cookie.Domain = FormsAuthentication.CookieDomain; }//设置cookie域

            this._httpContext.Response.Cookies.Add(cookie);
        }

        public SimplifiedUserInfo GetAuthenticatedUser()
        {
            SimplifiedUserInfo simplifiedUserInfo = null;
            if (this._httpContext?.Request == null
            || !this._httpContext.Request.IsAuthenticated || !(this._httpContext.User.Identity is FormsIdentity))
            {
                return simplifiedUserInfo;
            }

            //获取用户信息
            var formsIdentity = this._httpContext.User.Identity as FormsIdentity;
            if (formsIdentity != null)
            {
                simplifiedUserInfo = this.GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            }

            return simplifiedUserInfo;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            SessionHelper.Remove(ETPMSSetting.G_WorkContextSessionName);
        }
        #endregion

        #region Utils
        private SimplifiedUserInfo GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            SimplifiedUserInfo simplifiedUserInfo = null;
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            var userCode = ticket.Name;
            var userRoles = (ticket.UserData ?? string.Empty).Split(new char[] { ETPMSSetting.Spliter }, StringSplitOptions.RemoveEmptyEntries);
            if (string.IsNullOrWhiteSpace(userCode))
                return simplifiedUserInfo;

            var userEntity = this._userRepository.GetFirstOrDefualt(p => (!p.IS_DELETED && p.USER_CODE == userCode));
            if (userEntity != null)
            {
                simplifiedUserInfo = new SimplifiedUserInfo()
                {
                    UserId = userEntity.ID,
                    UserCode = userEntity.USER_CODE,
                    UserName = userEntity.USER_NAME,
                    PhoneNumber = userEntity.TELEPHONE,
                    EmailAddress = userEntity.EMAIL,
                    Sex = (Sex)userEntity.SEX,
                    DepartmentId = userEntity.DEPARTMENT_ID,
                    UserRoles = userRoles.ToList()
                };
            }

            return simplifiedUserInfo;
        }

        private IList<string> GetUserRoles(string userCode)
        {
            var roleCodes = (from k in this._userRoleRepository.GetAll()
                             join s in this._userRepository.GetAll() on k.USER_ID equals s.ID
                             join p in this._roleRepository.GetAll() on k.ROLE_ID equals p.ID
                             where s.USER_CODE == userCode && !s.IS_DELETED
                             select p.ROLE_CODE).ToList();

            return roleCodes;
        }
        #endregion
    }
}