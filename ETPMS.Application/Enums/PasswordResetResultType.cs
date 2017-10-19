using System.ComponentModel;

namespace ETPMS.Application.Enums
{
    public enum PasswordResetResultType : byte
    {
        /// <summary>
        /// 更新密码成功
        /// </summary>
        [Description("密码修改成功")]
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
        /// 密码不匹配
        /// </summary>
        [Description("密码不一致")]
        UnmatchedPassword = 4,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("用户不存在或已经离职")]
        Deleted = 5,

        /// <summary>
        /// 非法的密码格式
        /// </summary>
        [Description("非法的密码格式")]
        UnAvailablePasswordFormate = 6,

        /// <summary>
        /// 弱密码
        /// </summary>
        [Description("密码安全强度不够")]
        WeakPassword = 7
    }
}
