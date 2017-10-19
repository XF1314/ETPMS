using System;

namespace ETPMS.Application.DTOs
{
    public sealed class UserDto
    {
        public int? ID { get; set; }

        /// <summary>
        /// 编码(帐号)
        /// </summary>
        public string USER_CODE { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 所在部门Id
        /// </summary>
        public int DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TELEPHONE { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public byte SEX { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public byte USER_STATUS { get; set; }

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

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LAST_LOGIN_TIME { get; set; }

    }
}
