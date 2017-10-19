using ETPMS.Infrastructure.Logging;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.IO;

namespace ETPMS.Infrastructure.ThirdParty.Log4Net
{
    public sealed class Log4NetLoggerFactory : ILoggerFactory
    {
        public Log4NetLoggerFactory(string configFile)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }

            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else//控制台输出
            {
                BasicConfigurator.Configure(new ConsoleAppender { Layout = new PatternLayout() });
            }
        }

        public ILogger Create(Type type)=> new Log4NetLogger(LogManager.GetLogger(type));

        public ILogger Create(string name)=> new Log4NetLogger(LogManager.GetLogger(name));
    }
}
