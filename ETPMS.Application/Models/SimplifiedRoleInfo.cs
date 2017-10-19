using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Application.Models
{
    public sealed class SimplifiedRoleInfo
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
