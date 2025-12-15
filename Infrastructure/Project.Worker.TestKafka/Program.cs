using Project.Application.Contract.MessageBroker;
using Project.Host.Base.Bases;
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
                services.Configure<OptionKafka>(context.Configuration);
                services.AddHostedService<TestWorker>();
            });

            var host = builder.Build();
            host.Run();
        }
    }
}