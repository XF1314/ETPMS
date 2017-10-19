using System;

namespace ETPMS.Infrastructure.Serializing
{
    public sealed class UnImplementedJsonSerializer : IJsonSerializer
    {
        public object Deserialize(string value, Type type)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string value) where T : class
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
