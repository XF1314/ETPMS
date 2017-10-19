using ETPMS.Infrastructure.Components;
using System;
using System.ComponentModel;
using System.Linq;

namespace ETPMS.Infrastructure.Utilities
{
    public static class TypeUtils
    {
        public static T ConvertType<T>(object value)
        {
            if (value == null)
            {
                return default(T);
            }

            var typeConverter1 = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter1.CanConvertFrom(value.GetType()))
            {
                return (T)typeConverter1.ConvertFrom(value);
            }

            var typeConverter2 = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter2.CanConvertTo(typeof(T)))
            {
                return (T)typeConverter2.ConvertTo(value, typeof(T));
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static bool IsComponent(Type type)
        {
            return type.IsClass && !type.IsAbstract 
                && type.GetCustomAttributes(typeof(ComponentAttribute), false).Any();
        }
    }
}