using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace Project.Infastructure.Kafka.Producer
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null)
                return Array.Empty<byte>();

            // Nếu TValue là string thì KHÔNG serialize JSON nữa
            if (data is string str)
                return Encoding.UTF8.GetBytes(str);

            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}
