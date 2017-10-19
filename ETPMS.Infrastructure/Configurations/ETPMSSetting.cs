using System;

namespace ETPMS.Infrastructure.Configurations
{
    public sealed class ETPMSSetting
    {
        public const string G_SystemCode = "ETPMS";
        public const string G_SystemName = "设备测试项目管理信息系统";
        public const string G_UnAuthorizedRedirectUrl = "/Error/UnAuthorize";//未授权访问跳转地址
        public const string G_UnAuthenticatedRedirectUrl = "/Error/UnAuthenticate";//未认证访问跳转地址
        public const string G_GeneralErrorRedirectUrl = "/Error/General";//系统一般异常跳转地址
        public const string G_NotFoundRedirectUrl = "/Error/NotFound";//地址错误异常跳转地址
        public const string G_LoginUrl = "/Home/Login";//未登录时跳转地址
        public const string G_Salt = "asdfghjkl;'";
        public const string G_WorkContextSessionName = "ETPMS_WorkContext_Session_Name";


        public static readonly DateTime MinTime = DateTime.Parse("1900-1-1");
        public static readonly DateTime MaxTime = DateTime.Parse("9999-1-1");
        public const string PassWord = "123qwe";
        public const char Spliter = '|';
    }
}
