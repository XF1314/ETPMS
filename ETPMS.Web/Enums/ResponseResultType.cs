namespace ETPMS.Web.Enums
{
    public enum ResponseResultType : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succed = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 2,

        /// <summary>
        /// 出错
        /// </summary>
        Error = 3,

        /// <summary>
        /// 通知
        /// </summary>
        Info = 4
    }
}