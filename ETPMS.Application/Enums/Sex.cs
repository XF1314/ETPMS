using System.ComponentModel;

namespace ETPMS.Application.Enums
{
    public enum Sex : byte
    {
        /// <summary>
        /// 请选择
        /// </summary>
        [Description("请选择")]
        Unkown = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female
    }
}
