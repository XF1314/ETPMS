using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPMS.Infrastructure.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ComponentAttribute : Attribute
    {
        public LifeStyle LifeStyle { get; private set; }

        public ComponentAttribute() : this(LifeStyle.InstancePerDependency) { }


        public ComponentAttribute(LifeStyle lifeStyle)
        {
            this.LifeStyle = lifeStyle;
        }
    }
}
