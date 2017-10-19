using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ETPMS.Infrastructure.Serializing
{
    public sealed class DefaultBinarySerializer:IBinarySerializer
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public object Deserialize(byte[] data, Type type)
        {
            using (var stream = new MemoryStream(data))
            {
                return _binaryFormatter.Deserialize(stream);
            }
        }

        public T Deserialize<T>(byte[] data) where T : class
        {
            using (var stream = new MemoryStream(data))
            {
                return _binaryFormatter.Deserialize(stream) as T;
            }
        }
    }
}
