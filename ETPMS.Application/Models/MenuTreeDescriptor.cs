using Newtonsoft.Json;
using System.Collections.Generic;

namespace ETPMS.Application.Models
{
    public sealed class MenuTreeDescriptor
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string MenuName { get; set; }

        /// <summary>
        /// 跳转Url
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string TransferUrl { get; set; }

        /// <summary>
        /// 图标Url
        /// </summary>
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 附加样式
        /// </summary>
        [JsonProperty(PropertyName = "cssClass")]
        public string CssClass { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        [JsonProperty(PropertyName = "index")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<MenuTreeDescriptor> ChildMenus { get; set; }
    }
}
