using Microsoft.Extensions.Options;
using Project.Host.Base.Configs;
using Project.Host.Base.Lazyloads;
using Project.Worker.Base;

namespace Project.Worker.TestKafka.Service
{
    internal class TestWorker(ILazyloadProvider lazyloadProvider) : WorkerKafkaBase<TestWorker, string>(lazyloadProvider)
    {
        private IOptions<ConsumerConfig> ConsumerConfig => _lazyloadProvider.GetRequiredService<IOptions<ConsumerConfig>>();

        protected override int WorkerCount() => ConsumerConfig.Value.WorkerCount;

        protected override Task DoWork(string message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received message: {message}");
            return Task.CompletedTask;
        }

    }
}
