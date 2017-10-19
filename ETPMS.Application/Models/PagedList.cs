using System.Collections.Generic;

namespace ETPMS.Application.Models
{
    public sealed class PagedList<TDto>
    {
        public PagedList(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = 0;
            this.Items = new List<TDto>();
        }

        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 页面记录条数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总的记录条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页面记录
        /// </summary>
        public List<TDto> Items { get; set; }
    }
}
