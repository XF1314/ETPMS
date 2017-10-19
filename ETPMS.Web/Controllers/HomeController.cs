using ETPMS.Application.Contracts;
using ETPMS.Application.Enums;
using ETPMS.Application.Implementations;
using ETPMS.Infrastructure.Extensions;
using ETPMS.Web.Attributes;
using ETPMS.Web.Extensions;
using ETPMS.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ETPMS.Web.Controllers
{
    public sealed class HomeController : ETPMSBaseController
    {
        public ViewResult Index()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        #region 登录和登出
        [Anonymous]
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost, Anonymous]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            try
            {
                var validationResult = new LoginModelValidator().Validate(loginModel);
                if (validationResult.IsValid)
                {
                    var userService = ServiceContainer.Resolve<IUserService>();
                    var userValidateResult = userService.ValidateUser(loginModel.UserCode, loginModel.PassWord, PasswordFormatType.DESEncrypted);
                    if (userValidateResult == UserValidateResultType.Successful)//TODO:状态保持，页面跳转
                    {
                        var formsAuthenticationService = ServiceContainer.Resolve<FormsAuthenticationService>();
                        formsAuthenticationService.SignIn(loginModel.UserCode.Trim(), loginModel.RememberMe);
                        Logger.Info($"用户-{loginModel.UserCode}成功登录系统");
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag._errorMsgDisplay = true; //默认设置前台呈现错误信息
                        ModelState.AddModelError("", userValidateResult.GetDescription());
                        return View(loginModel);
                    }
                }
                else
                {
                    ViewBag._errorMsgDisplay = true;
                    ModelState.AddModelError("", validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "参数错误~");
                    return View(loginModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag._errorMsgDisplay = true; //默认设置前台呈现错误信息
                ModelState.AddModelError("", $"用户-{loginModel.UserCode}登录失败");
                Logger.Error($"用户-{loginModel.UserCode}登录失败", ex);
                return View(loginModel);
            }
        }

        public ActionResult Logout()
        {
            try
            {
                var formsAuthenticationService = base.ServiceContainer.Resolve<FormsAuthenticationService>();
                formsAuthenticationService.SignOut();//登出
            }
            catch (Exception ex)
            {
                Logger.Error($"用户-{CurrentUser.UserCode}登出失败", ex);
            }

            return RedirectToAction("Login");
        }
        #endregion

        public ViewResult Dashboard()
        {
            return View();
        }

        public ViewResult Copyright()
        {
            return View();
        }

        public ViewResult Empty()
        {
            return View();
        }

        #region 异常

        [Anonymous]
        public ViewResult GeneralException()
        {
            return View();
        }

        [Anonymous]
        public ViewResult NotFoundException()
        {
            return View();
        }


        [Anonymous]
        public ViewResult UnAuthorizedException()
        {
            return View();
        }

        [Anonymous]
        public ViewResult UnAuthenticatedException()
        {
            return View();
        }
        #endregion
    }
}