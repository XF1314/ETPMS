using System.ComponentModel;

namespace ETPMS.Application.Enums
{
    public enum UserStatus : byte
    {
        /// <summary>
        /// 请选择
        /// </summary>
        [Description("请选择")]
        UnCertain = 0,

        /// <summary>
        /// 已激活
        /// </summary>
        [Description("已激活")]
        IsActived,

        /// <summary>
        /// 未激活
        /// </summary>
        [Description("未激活")]
        UnActived,

        /// <summary>
        /// 未激活
        /// </summary>
        [Description("已锁定")]
        IsLocked,
    }
}
