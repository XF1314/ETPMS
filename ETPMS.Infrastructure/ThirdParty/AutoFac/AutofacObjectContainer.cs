using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using ETPMS.Infrastructure.Components;
using System;
using System.Web;

namespace ETPMS.Infrastructure.ThirdParty.AutoFac
{
    public sealed class AutofacObjectContainer : IObjectContainer
    {
        private IContainer _container;

        public AutofacObjectContainer(IContainer container)
        {
            this._container = container;
        }

        public TObject Resolve<TObject>(ILifetimeScope lifetimeScope = null) where TObject : class
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.Resolve<TObject>();
        }

        public bool TryResolve<TObject>(out TObject instance, ILifetimeScope lifetimeScope = null) where TObject : class
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.TryResolve(out instance);
        }

        public TObject ResolveNamed<TObject>(string serviceName, ILifetimeScope lifetimeScope = null) where TObject : class
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.ResolveNamed<TObject>(serviceName);
        }

        public bool TryResolveNamed<TObject>(string serviceName, out TObject instance, ILifetimeScope lifetimeScope = null) where TObject : class
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            instance = lifetimeScope.ResolveNamed<TObject>(serviceName);
            return instance != null;
        }

        public object Resolve(Type objectType, ILifetimeScope lifetimeScope = null)
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.Resolve(objectType);
        }

        public bool TryResolve(Type objectType, out object instance, ILifetimeScope lifetimeScope = null)
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.TryResolve(objectType, out instance);
        }

        public object ResolveNamed(string objectName, Type objectType, ILifetimeScope lifetimeScope = null)
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.ResolveNamed(objectName, objectType);
        }

        public bool TryResolveNamed(string objectName, Type objectType, out object instance, ILifetimeScope lifetimeScope = null)
        {
            lifetimeScope = lifetimeScope ?? this.GetLifetimeScope();
            return lifetimeScope.TryResolveNamed(objectName, objectType, out instance);
        }

        public ILifetimeScope GetLifetimeScope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                return this._container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                return this._container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }
    }
}
