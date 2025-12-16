using Confluent.Kafka;
using System.Text.Json;

namespace Project.Application.Contract.MessageBroker
{
    public class JsonDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull || data.IsEmpty) return default!;
            return JsonSerializer.Deserialize<T>(data)!;
        }
    }

}
