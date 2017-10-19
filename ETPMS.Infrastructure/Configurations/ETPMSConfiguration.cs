using Autofac;
using Autofac.Integration.Mvc;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.IO;
using ETPMS.Infrastructure.Logging;
using ETPMS.Infrastructure.Repository;
using ETPMS.Infrastructure.Serializing;
using ETPMS.Infrastructure.ThirdParty.AutoFac;
using ETPMS.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ETPMS.Infrastructure.Configurations
{
    public sealed class ETPMSConfiguration
    {
        public static ETPMSConfiguration Instance { get; private set; }
        private ContainerBuilder _containerBuilder;

        private ETPMSConfiguration(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public static ETPMSConfiguration Create(ContainerBuilder containerBuilder)
        {
            Ensure.NotNull(containerBuilder, "ContainerBuilder");
            Instance = new ETPMSConfiguration(containerBuilder);
            return Instance;
        }

        public void Initialize()
        {
            var container = this._containerBuilder.Build();
            ObjectContainer.SetContainer(new AutofacObjectContainer(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #region ComponentRegisters
        /// <summary>
        /// 公共组件注册
        /// </summary>
        public ETPMSConfiguration RegisterCommonComponents()
        {
            SetDefaultComponent<IOHelper, IOHelper>();
            SetDefaultComponent<ILoggerFactory, NullLoggerFactory>();
            SetDefaultComponent<IBinarySerializer, DefaultBinarySerializer>();
            SetDefaultComponent<IJsonSerializer, UnImplementedJsonSerializer>();
            return this;
        }

        /// <summary>
        /// 业务组件注册
        /// </summary>
        public ETPMSConfiguration RegisterBussinessComponents(params Assembly[] assemblies)
        {
            var registedComponentTypes = new List<Type>();
            if (assemblies.Any())
            {
                assemblies.ToList().ForEach(k =>
                {
                    var kinds = k.GetTypes();
                    var temps = kinds.Where(TypeUtils.IsComponent);
                    foreach (var type in temps)
                    {
                        if (!registedComponentTypes.Contains(type))
                        {
                            RegisterComponentType(type);
                        }
                    }
                });
            }
            return this;
        }

        public ETPMSConfiguration RegisterRepositoryComponents()
        {
            // this._containerBuilder.Register(o => new ETPMSDbSession()).InstancePerLifetimeScope();
            _containerBuilder.RegisterGeneric(typeof(ETPMSBaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            return this;
        }

        /// <summary>
        /// 未处理异常处理器注册
        /// </summary>
        /// <returns></returns>
        public ETPMSConfiguration RegisterUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
                logger.ErrorFormat("未处理异常: {0}", e.ExceptionObject);
            };
            return this;
        }

        /// <summary>
        /// 默认组件设置
        /// </summary>
        /// <typeparam name="TType">类型</typeparam>
        /// <typeparam name="TImplementer">继承/实现类型</typeparam>
        /// <param name="componentName">组件名称</param>
        /// <param name="lifeStyle">生命周期</param>
        /// <returns></returns>
        public ETPMSConfiguration SetDefaultComponent<TType, TImplementer>(string componentName = null, LifeStyle lifeStyle = LifeStyle.SingleInstance)
            where TType : class
            where TImplementer : class, TType
        {
            var registrationBuilder = this._containerBuilder.RegisterType<TImplementer>().As<TType>();
            if (lifeStyle == LifeStyle.InstancePerLifetimeScope) { registrationBuilder.InstancePerLifetimeScope(); }
            if (lifeStyle == LifeStyle.SingleInstance) { registrationBuilder.SingleInstance(); }

            return this;
        }

        /// <summary>
        /// 默认组件设置
        /// </summary>
        /// <typeparam name="TType">实体类型</typeparam>
        /// <typeparam name="TImplementer">继承/实现类型</typeparam>
        /// <param name="instance">实体</param>
        /// <param name="componentName">组件名称</param>
        /// <returns></returns>
        public ETPMSConfiguration SetDefaultComponent<TType, TImplementer>(TImplementer instance, string componentName = null)
            where TType : class
            where TImplementer : class, TType
        {
            var registrationBuilder = this._containerBuilder.RegisterInstance<TImplementer>(instance).As<TType>().SingleInstance();
            if (componentName != null) { registrationBuilder.Named<TType>(componentName); }

            return this;
        }
        #endregion

        #region Utils
        private void RegisterComponentType(Type type)
        {
            var lifeStyle = ParseComponentLife(type);
            this.RegisterType(type, null, lifeStyle);
            foreach (var interfaceType in type.GetInterfaces())
            {
                this.RegisterType(interfaceType, type, null, lifeStyle);
            }
        }

        private void RegisterType(Type implementationType, string objectName = null, LifeStyle lifeStyle = LifeStyle.SingleInstance)
        {
            var registrationBuilder = this._containerBuilder.RegisterType(implementationType);

            if (objectName != null) { registrationBuilder.Named(objectName, implementationType); }
            if (lifeStyle == LifeStyle.SingleInstance) { registrationBuilder.SingleInstance(); }
            if (lifeStyle == LifeStyle.InstancePerLifetimeScope) { registrationBuilder.SingleInstance(); }
        }

        private void RegisterType(Type objectType, Type implementationType, string objectName = null, LifeStyle lifeStyle = LifeStyle.SingleInstance)
        {
            var registrationBuilder = this._containerBuilder.RegisterType(implementationType).As(objectType);

            if (objectName != null) { registrationBuilder.Named(objectName, implementationType); }
            if (lifeStyle == LifeStyle.SingleInstance) { registrationBuilder.SingleInstance(); }
            if (lifeStyle == LifeStyle.InstancePerLifetimeScope) { registrationBuilder.SingleInstance(); }
        }

        private static LifeStyle ParseComponentLife(Type type)
        {
            var attributes = type.GetCustomAttributes<ComponentAttribute>(false);
            if (attributes != null && attributes.Any())
            {
                return attributes.First().LifeStyle;
            }
            return LifeStyle.SingleInstance;
        }

        #endregion
    }
}
