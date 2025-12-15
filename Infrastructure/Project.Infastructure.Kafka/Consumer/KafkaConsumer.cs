using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Project.Application.Contract.MessageBroker;

namespace Project.Infastructure.Kafka.Consumer
{
    public class KafkaConsumer<TValue> : IMessageConsumer<TValue>
    {
        private readonly IConsumer<string, TValue> _consumer;
        private readonly string _topic;

        public KafkaConsumer(IOptions<OptionKafka> optionKafka, IDeserializer<TValue> value)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = optionKafka.Value.ConnectionKafka,
                GroupId = optionKafka.Value.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                SessionTimeoutMs = 6000,
                MaxPollIntervalMs = 300000
            };

            _consumer = new ConsumerBuilder<string, TValue>(config)
                .SetValueDeserializer(value)
                .SetErrorHandler((_, e) => Console.WriteLine($"❌ Error: {e.Reason}"))
                .Build();

            _topic = optionKafka.Value.Topic;
        }

        public async Task ConsumeAsync(Func<TValue, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);
            Console.WriteLine($"🎧 Subscribed to topic: {_topic}");

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    ConsumeResult<string, TValue>? result = null;

                    try
                    {
                        result = _consumer.Consume(cancellationToken);

                        await messageHandler(result.Message.Value, cancellationToken);

                        _consumer.Commit(result);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Kafka error: {ex.Error.Reason}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Business error at {result?.TopicPartitionOffset}: {ex}");
                        // không commit → Kafka retry
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
