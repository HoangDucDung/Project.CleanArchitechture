using Microsoft.Extensions.Options;
using Project.Application.Contract.MessageBroker;
using Project.Host.Base.Lazyloads;
using Project.Worker.Base;

namespace Project.Worker.TestKafka.Service
{
    internal class TestWorker(ILazyloadProvider lazyloadProvider) : WorkerKafkaBase<TestWorker, string>(lazyloadProvider)
    {

        protected override Task DoWork(string message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received message: {message}");
            return Task.CompletedTask;
        }
    }
}
