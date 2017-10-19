namespace ETPMS.Application.Models
{
    public sealed class PageDescriptor
    {
        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 是否升序
        /// </summary>
        public bool IsAscending { get; set; }
    }
}
