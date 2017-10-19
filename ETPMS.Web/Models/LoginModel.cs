using FluentValidation.Attributes;
using System.ComponentModel;

namespace ETPMS.Web.Models
{
    public class LoginModel
    {
        [DisplayName("用户名")]
        public string UserCode { get; set; }

        [DisplayName("密码")]
        public string PassWord { get; set; }

        [DisplayName("记住我")]
        public bool RememberMe { get; set; }

    }
}