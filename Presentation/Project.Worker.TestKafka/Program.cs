using Project.Application;
using Project.Application.Contract.Models.MessageBroker;
using Project.Host.Base.Bases;
using Project.Host.Base.Lazyloads;
using Project.Worker.TestKafka.Service;

namespace Project.Worker.TestKafka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddBaseConfiguration([
                    "kafka.json",
                ]);
            });

            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ILazyloadProvider, LazyloadProvider>();
                services.UseMessageBrokerFactory();
                services.Configure<OptionKafka>(context.Configuration.GetSection("OptionKafka"));
                services.AddHostedService<TestWorker>();
            });

            var host = builder.Build();
            host.Run();
        }
    }
}