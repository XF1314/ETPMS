using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Application.DTOs
{
    public sealed class PasswordChangeDto
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 原始密码
        /// </summary>
        public string OriginalPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }

    }
}
