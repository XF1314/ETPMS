using System;
namespace ETPMS.Infrastructure.Logging
{
    public sealed class NullLoggerFactory:ILoggerFactory
    {
        private static readonly NullLogger Logger = new NullLogger();

        public ILogger Create(string name)
        {
            return Logger;
        }

        public ILogger Create(Type type)
        {
            return Logger;
        }
    }
}
