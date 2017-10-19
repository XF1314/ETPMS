using Autofac;
using Autofac.Integration.Mvc;
using ETPMS.Infrastructure.Utilities;
using System.Reflection;

namespace ETPMS.Web.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependency(ContainerBuilder containerBuilder)
        {
            Ensure.NotNull(containerBuilder, "ContainerBuilder");
            containerBuilder = containerBuilder ?? new ContainerBuilder();
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly());
            //containerBuilder.RegisterGeneric(typeof(ETPMSBaseService<>)).As(typeof(IETPMSBaseService<>)).InstancePerLifetimeScope();
        }
    }
}