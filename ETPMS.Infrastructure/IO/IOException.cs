using System;

namespace ETPMS.Infrastructure.IO
{
    [Serializable]
    public class IOException : Exception
    {
        public IOException() : base() { }

        public IOException(string message) : base(message) { }

        public IOException(string message, Exception innerException) : base(message, innerException) { }

        public IOException(string message, params object[] args) : base(string.Format(message, args)) { }

        public IOException(string message, Exception innerException, params object[] args) : base(string.Format(message, args), innerException) { }
    }
}
