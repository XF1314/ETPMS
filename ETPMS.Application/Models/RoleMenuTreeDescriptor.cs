using Newtonsoft.Json;
using System.Collections.Generic;

namespace ETPMS.Application.Models
{
    public sealed class RoleMenuTreeDescriptor
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int MenuID { get; set; }

        /// <summary>
        /// 菜单Code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单是否默认展开
        /// </summary>
        [JsonProperty(PropertyName = "expanded")]
        public bool Expanded { get; set; }

        /// <summary>
        /// 菜单Url
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [JsonProperty(PropertyName = "spriteCssClass")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 菜单是否默认选中
        /// </summary>
        [JsonProperty(PropertyName = "checked")]
        public bool Checked { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        [JsonProperty(PropertyName = "index")]
        public byte SortIndex { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        [JsonProperty(PropertyName = "fatherMenuCode")]
        public int FatherMenuId { get; set; }

        /// <summary>
        /// 是否拥有子菜单
        /// </summary>
        [JsonProperty(PropertyName = "hasItem")]
        public bool HasChildMenus { get; set; }

        /// <summary>
        /// 拥有的子菜单
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public IList<RoleMenuTreeDescriptor> ChildMenus { get; set; }
    }
}
