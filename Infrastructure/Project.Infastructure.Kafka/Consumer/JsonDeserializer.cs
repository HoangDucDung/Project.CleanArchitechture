using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Project.Application.Contract.MessageBroker
{
    public class JsonDeserializer<T> : IDeserializer<T>
    {
        private readonly ILogger<JsonDeserializer<T>> _logger;

        public JsonDeserializer(ILogger<JsonDeserializer<T>> logger)
        {
            _logger = logger;
        }

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull || data.IsEmpty) return default!;

            if (typeof(T) == typeof(string))
                return (T)(object)Encoding.UTF8.GetString(data);

            try
            {
                return JsonSerializer.Deserialize<T>(data)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deserialize message to type {Type}, returning raw string instead.", typeof(T).FullName);
                return (T)(object)Encoding.UTF8.GetString(data);
            }
        }
    }

}
