using Project.Host.Base.Bases;
using Project.Host.Base.Lazyloads;
using Project.Infastructure.Kafka;
using Project.Worker.TestKafka.Service;
using Project.Host.Base.Configs;

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
                // Đăng ký các options
                services.GetKafkaConfig(context.Configuration);
                services.GetProducerCommonConfig(context.Configuration);

                services.AddHostedService<TestWorker>();
            });

            var host = builder.Build();
            host.Run();
        }
    }
}