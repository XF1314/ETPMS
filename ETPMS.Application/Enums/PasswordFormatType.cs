using System.ComponentModel;

namespace ETPMS.Application.Enums
{
    public enum PasswordFormatType : byte
    {
        /// <summary>
        /// 未进行加密处理
        /// </summary>
        [Description("文本")]
        Plain = 0,

        /// <summary>
        /// MD5加密
        /// </summary>
        [Description("MD5加密")]
        MD5Encrypted = 1,

        /// <summary>
        /// 哈希加密
        /// </summary>
        [Description("哈希加密")]
        Hashed = 2,

        /// <summary>
        ///  DES加密
        /// </summary>
        [Description("DES加密")]
        DESEncrypted = 3
    }
}
