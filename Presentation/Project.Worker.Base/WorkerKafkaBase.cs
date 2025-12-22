using Project.Application.Contract.MessageBroker;
using Project.Host.Base.Lazyloads;

namespace Project.Worker.Base
{
    public abstract class WorkerKafkaBase<TService, TValue> : BackgroundService
    {
        protected readonly IMessageConsumer<TValue> _consumer;
        protected readonly ILogger<TService> _logger;

        protected WorkerKafkaBase(ILazyloadProvider lazyloadProvider)
        {
            _consumer = lazyloadProvider.GetRequiredService<IMessageConsumer<TValue>>();
            _logger = lazyloadProvider.GetRequiredService<ILogger<TService>>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => _consumer.ConsumeAsync(HandleMessageAsync, stoppingToken);

        private async Task HandleMessageAsync(TValue message, CancellationToken token)
        {
            try
            {
                await DoWork(message, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kafka message processing failed");
                throw;
            }
        }

        protected abstract Task DoWork(TValue message, CancellationToken cancellationToken);

        protected virtual Task messageHandler(TValue message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received message: {message}", message);
            return Task.CompletedTask;
        }
    }
}
