using System;

namespace ETPMS.Infrastructure.Logging
{
   public  interface ILoggerFactory
    {
        ILogger Create(string name);

        ILogger Create(Type type);
    }
}
