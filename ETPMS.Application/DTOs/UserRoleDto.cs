using System;

namespace ETPMS.Application.DTOs
{
    public sealed class UserRoleDto
    {
        public int ID { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int ROLE_ID { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public int CREATER_ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATE_TIME { get; set; }

    }
}
