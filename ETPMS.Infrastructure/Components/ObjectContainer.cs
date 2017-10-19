using Autofac;
using System;

namespace ETPMS.Infrastructure.Components
{
    public sealed class ObjectContainer
    {
        public static IObjectContainer Current { get; private set; }
        public static void SetContainer(IObjectContainer container)
        {
            Current = container;
        }

        public static TObject Resolve<TObject>(ILifetimeScope lifetimeScope = null) where TObject : class
        {
            return Current.Resolve<TObject>(lifetimeScope);
        }

        public static bool TryResolve<TObject>(out TObject instance, ILifetimeScope lifetimeScope = null) where TObject : class
        {
            return Current.TryResolve<TObject>(out instance, lifetimeScope);
        }

        public static TObject ResolveNamed<TObject>(string ObjectName, ILifetimeScope lifetimeScope = null) where TObject : class
        {
            return Current.ResolveNamed<TObject>(ObjectName);
        }

        public static object Resolve(Type ObjectType, ILifetimeScope lifetimeScope)
        {
            return Current.Resolve(ObjectType, lifetimeScope);
        }

        public static bool TryResolve(Type ObjectType, out object instance, ILifetimeScope lifetimeScope = null)
        {
            return Current.TryResolve(ObjectType, out instance, lifetimeScope);
        }

        public static object ResolveNamed(string ObjectName, Type ObjectType, ILifetimeScope lifetimeScope = null)
        {
            return Current.ResolveNamed(ObjectName, ObjectType, lifetimeScope);
        }

        public static bool TryResolveNamed(string ObjectName, Type ObjectType, out object instance, ILifetimeScope lifetimeScope = null)
        {
            return Current.TryResolveNamed(ObjectName, ObjectType, out instance, lifetimeScope);
        }
    }
}
