using System.ComponentModel;

namespace ETPMS.Application.Enums
{
    public enum UserValidateResultType : byte
    {
        /// <summary>
        /// 验证通过
        /// </summary>
        [Description("验证通过")]
        Successful = 1,

        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("没有找到用户帐户")]
        NotExist = 2,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        WrongPassword = 3,

        /// <summary>
        /// 未激活
        /// </summary>
        [Description("帐户未激活")]
        NotActive = 4,

        /// <summary>
        /// 已锁定
        /// </summary>
        [Description("帐户已被锁定")]
        Locked = 5,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("用户不存在或已经离职")]
        Deleted = 6,

        /// <summary>
        /// 未注册
        /// </summary>
        [Description("帐户未注册")]
        NotRegistered = 7,

        /// <summary>
        /// 非内部员工
        /// </summary>
        [Description("非内部员工帐户")]
        NotEmployees = 8,

        /// <summary>
        /// 非法的密码格式
        /// </summary>
        [Description("非法的密码格式")]
        UnAvailablePasswordFormate = 9,
    }
}
