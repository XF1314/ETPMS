using Autofac;
using ETPMS.Infrastructure.Components;
using ETPMS.Infrastructure.Configurations;
using ETPMS.Infrastructure.Logging;
using ETPMS.Infrastructure.Utilities;
using ETPMS.Web.App_Start;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using OSharp.Web.Mvc.Binders;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ETPMS.Web
{
    public class MvcApplication : HttpApplication
    {
        private ILogger _logger;

        protected void Application_Start()
        {
            var containerBuilder = new ContainerBuilder();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.RegisterDependency(containerBuilder);

            //初始化系统组件
            this.InitializeETPMS(containerBuilder);
            //设置默认模型绑定
            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());
            //注册 FluentValidation 模型验证
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;//验证失败后不再继续进行后续验证
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;//设置Attribute验证无效
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));//将FluentValidation作为默认的模型验证方式

            this._logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            this._logger.Info("ETPMS initialized.");
        }

        private void InitializeETPMS(ContainerBuilder containerBuilder)
        {
            Ensure.NotNull(containerBuilder, "ContainerBuilder");
            var assemblies = new[]
            {
                Assembly.Load("ETPMS.Web"),
                Assembly.Load("ETPMS.Application"),
                Assembly.Load("ETPMS.Infrastructure")
            };

            ETPMSConfiguration
                .Create(containerBuilder)
                .RegisterCommonComponents()
                .RegisterRepositoryComponents()
                .RegisterBussinessComponents(assemblies)
                .RegisterUnhandledExceptionHandler()
                .UseJsonNet()
                .UseLog4Net()
                .Initialize();//注：该方法必须执行，且必须置于最后执行
        }
    }
}
