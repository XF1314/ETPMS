using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Infrastructure.Components
{
    public enum LifeStyle : byte
    {
        /// <summary>
        /// 瞬态(每次调用返回一个新的实例)，默认的生命周期形态
        /// </summary>
        InstancePerDependency = 0,

        /// <summary>
        /// 请求周期唯一
        /// </summary>
        InstancePerLifetimeScope,

        /// <summary>
        /// 单例(每次调用都返回同一个实例)
        /// </summary>
        SingleInstance
    }
}
