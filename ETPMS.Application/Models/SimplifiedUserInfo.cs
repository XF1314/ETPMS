using ETPMS.Application.Enums;
using System.Collections.Generic;

namespace ETPMS.Application.Models
{
    public sealed class SimplifiedUserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 所具有的用户角色
        /// </summary>
        public List<string> UserRoles { get; set; }
    }
}
