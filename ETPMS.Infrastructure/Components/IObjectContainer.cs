using Autofac;
using System;

namespace ETPMS.Infrastructure.Components
{
    public interface IObjectContainer
    {
        object Resolve(Type objectType,ILifetimeScope lifetimeScope=null);

        bool TryResolve(Type objectType, out object instance, ILifetimeScope lifetimeScope = null);

        object ResolveNamed(string objectName, Type objectType, ILifetimeScope lifetimeScope = null);

        bool TryResolveNamed(string objectName, Type objectType, out object instance, ILifetimeScope lifetimeScope = null);

        TObject Resolve<TObject>( ILifetimeScope lifetimeScope = null) where TObject : class;

        bool TryResolve<TObject>(out TObject instance, ILifetimeScope lifetimeScope = null) where TObject : class;

        TObject ResolveNamed<TObject>(string ojectName, ILifetimeScope lifetimeScope = null) where TObject : class;

        bool TryResolveNamed<TObject>(string objectName, out TObject instance, ILifetimeScope lifetimeScope = null) where TObject : class;
    }
}
