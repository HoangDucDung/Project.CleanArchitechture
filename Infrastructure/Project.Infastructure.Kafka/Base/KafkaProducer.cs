using Confluent.Kafka;
using System.Text.Json;

namespace Project.Infastructure.Kafka.Base
{
    public class KafkaProducer<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly string _topic;

        public KafkaProducer(string bootstrapServers, string topic, ISerializer<TKey> key, ISerializer<TValue> value)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                Acks = Acks.All,
                EnableIdempotence = true,
                MaxInFlight = 5,
                MessageSendMaxRetries = 10,
                CompressionType = CompressionType.Snappy
            };

            _producer = new ProducerBuilder<TKey, TValue>(config)
                //.SetKeySerializer(key) // Xem xét có cần key để chỉ định partition không
                .SetValueSerializer(value)
                .Build();

            _topic = topic;
        }

        public async Task<DeliveryResult<TKey, TValue>> ProduceAsync(TKey key, TValue value)
        {
            try
            {
                var message = new Message<TKey, TValue>
                {
                    Key = key,
                    Value = value,
                    Timestamp = Timestamp.Default
                };

                var result = await _producer.ProduceAsync(_topic, message);

                Console.WriteLine($"✅ Message delivered to {result.TopicPartitionOffset}");
                return result;
            }
            catch (ProduceException<TKey, TValue> ex)
            {
                Console.WriteLine($"❌ Delivery failed: {ex.Error.Reason}");
                throw;
            }
        }

        public void Dispose()
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }
    }
}
