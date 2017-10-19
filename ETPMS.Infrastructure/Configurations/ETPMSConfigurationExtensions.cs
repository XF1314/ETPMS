using ETPMS.Infrastructure.Logging;
using ETPMS.Infrastructure.Serializing;
using ETPMS.Infrastructure.ThirdParty.JsonNet;
using ETPMS.Infrastructure.ThirdParty.Log4Net;

namespace ETPMS.Infrastructure.Configurations
{
    public static class ETPMSConfigurationExtensions
    {
        /// <summary>
        /// 使用JsonNet 作为Json序列化工具
        /// </summary>
        public static ETPMSConfiguration UseJsonNet(this ETPMSConfiguration configuration)
        {
            configuration.SetDefaultComponent<IJsonSerializer, NewtonsoftJsonSerializer>(new NewtonsoftJsonSerializer());
            return configuration;
        }

        /// <summary>
        /// 使用Log4Net 作为日志记录工具
        /// </summary>
        public static ETPMSConfiguration UseLog4Net(this ETPMSConfiguration configuration)
        {
            return UseLog4Net(configuration, "log4net.config");
        }

        /// <summary>
        /// 使用Log4Net 作为日志记录工具
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <returns></returns>
        public static ETPMSConfiguration UseLog4Net(this ETPMSConfiguration configuration, string configFile)
        {
            configuration.SetDefaultComponent<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));
            return configuration;
        }
    }
}
