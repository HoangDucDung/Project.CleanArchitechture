using Confluent.Kafka;
using System.Text.Json;

namespace Project.Infastructure.Kafka.Base
{
    public class KafkaConsumer<TKey, TValue>
    {
        private readonly IConsumer<Ignore, TValue> _consumer;
        private readonly string _topic;

        public KafkaConsumer(string bootstrapServers, string groupId, string topic, IDeserializer<TValue> value)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                SessionTimeoutMs = 6000,
                MaxPollIntervalMs = 300000
            };

            _consumer = new ConsumerBuilder<Ignore, TValue>(config)
                .SetValueDeserializer(value)
                .SetErrorHandler((_, e) => Console.WriteLine($"❌ Error: {e.Reason}"))
                .Build();

            _topic = topic;
        }

        public async Task ConsumeAsync(Func<TValue, Task> messageHandler, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            Console.WriteLine($"🎧 Subscribed to topic: {_topic}");

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);

                        if (consumeResult != null)
                        {
                            Console.WriteLine($"📨 Received message at {consumeResult.TopicPartitionOffset}");

                            // Process message
                            await messageHandler(consumeResult.Message.Value);

                            // Commit offset
                            _consumer.Commit(consumeResult);
                            _consumer.StoreOffset(consumeResult);
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"❌ Consume error: {ex.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("⚠️ Consumer cancelled");
            }
            finally
            {
                _consumer.Close();
            }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}
