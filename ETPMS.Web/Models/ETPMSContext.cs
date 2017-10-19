using ETPMS.Application.Models;
using ETPMS.Infrastructure.Components;

namespace ETPMS.Web.Models
{
    public sealed class ETPMSContext
    {
        public ETPMSContext(bool isAnonymous)
        {
            this.IsAnonymous = isAnonymous;
        }

        /// <summary>
        /// 是否匿名访问
        /// </summary>
        public bool IsAnonymous { get; private set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public SimplifiedUserInfo CurrentUser { get; set; }

        /// <summary>
        /// 依赖解析器
        /// </summary>
        public IObjectContainer ObjectContainer { get; set; }
    }
}