using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Application.Contract.MessageBroker;
using Project.Application.Contract.Models.MessageBroker;
using Project.Extensions.Extensions;
using Project.Libs.Exceptions;

namespace Project.Infastructure.Kafka.Producer
{
    public class KafkaProducer<TKey, TValue> : IMessageProducer<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly string _topic;

        private readonly ILogger<KafkaProducer<TKey, TValue>> _logger;
        public KafkaProducer(IOptions<KafkaConfig> KafkaConfig, IOptions<ProducerCustomConfig> ProducerCommon, ISerializer<TKey> key, ISerializer<TValue> value, ILogger<KafkaProducer<TKey, TValue>> logger)
        {
            if (KafkaConfig.Value == null) throw new BusinessException("Không lấy được Kafka producer options.");

            if ((ProducerCommon.Value == null || ProducerCommon.Value.BootstrapServers.IsNullOrEmpty()) && KafkaConfig.Value?.Producer == null) throw new BusinessException("Không cấu hình Kafka producer options.");

            //Lấy cấu hình chung nếu kafka với options topic đó không cáu hình
            if (KafkaConfig.Value?.Producer == null)
            {
                KafkaConfig.Value!.Producer = ProducerCommon.Value;
            }

            var config = new ProducerConfig
            {
                BootstrapServers = KafkaConfig.Value.Producer!.BootstrapServers,
                Acks = Acks.All,
                EnableIdempotence = true,
                MaxInFlight = 5,
                MessageSendMaxRetries = 10,
                CompressionType = CompressionType.Snappy
            };

            _logger = logger;

            _producer = new ProducerBuilder<TKey, TValue>(config)
                .SetKeySerializer(key)
                .SetValueSerializer(value)
                .SetErrorHandler((_, e) => _logger.LogError($"Kafka Error: {e.Reason}"))
                .SetLogHandler((_, log) => _logger.LogInformation($"Kafka Log: {log.Message}"))
                .Build();

            _topic = KafkaConfig.Value.Producer.Topic;
        }

        public async Task ProduceAsync(TKey key, TValue value)
        {
            try
            {
                var message = new Message<TKey, TValue>
                {
                    Key = key,
                    Value = value,
                    Timestamp = Timestamp.Default
                };

                await _producer.ProduceAsync(_topic, message);

                Console.WriteLine($"✅ Message delivered");
            }
            catch (ProduceException<TKey, TValue> ex)
            {
                Console.WriteLine($"❌ Delivery failed: {ex.Error.Reason}");
                throw;
            }
        }

        //public async Task<DeliveryResult<TKey, TValue>> ProduceDeliveryAsync(TKey key, TValue value)
        //{
        //    try
        //    {
        //        var message = new Message<TKey, TValue>
        //        {
        //            Key = key,
        //            Value = value,
        //            Timestamp = Timestamp.Default
        //        };

        //        var result = await _producer.ProduceAsync(_topic, message);

        //        Console.WriteLine($"✅ Message delivered to {result.TopicPartitionOffset}");
        //        return result;
        //    }
        //    catch (ProduceException<TKey, TValue> ex)
        //    {
        //        Console.WriteLine($"❌ Delivery failed: {ex.Error.Reason}");
        //        throw;
        //    }
        //}

        public void Dispose()
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }
    }
}
