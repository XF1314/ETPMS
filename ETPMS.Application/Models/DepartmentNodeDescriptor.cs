using Newtonsoft.Json;

namespace ETPMS.Application.Models
{
    public sealed class DepartmentNodeDescriptor
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 上级部门Id
        /// </summary>
        [JsonProperty(PropertyName = "pId")]
        public int FatherDepartmentId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否拥有子部门
        /// </summary>
        [JsonProperty(PropertyName = "hasItem")]
        public bool HasChildDepartments { get; set; }

        /// <summary>
        /// ImgUrl
        /// </summary>
        [JsonProperty(PropertyName = "spriteCssClass")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        [JsonProperty(PropertyName = "index")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonProperty(PropertyName = "expanded")]
        public bool Expanded { get; set; }

    }
}
