using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Application.DTOs
{
    public sealed class RoleDto
    {
        public int? ID { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string ROLE_CODE { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string ROLE_NAME { get; set; }

        /// <summary>
        /// 角色描述 
        /// </summary>
        public string ROLE_DESCRIPTION { get; set; }

        /// <summary>
        /// 角色索引
        /// </summary>
        public int ROLE_INDEX { get; set; }

        /// <summary>
        /// 最后操作人Id
        /// </summary>
        public int? OPERATOR_ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATE_TIME { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UPDATE_TIME { get; set; }
    }
}
