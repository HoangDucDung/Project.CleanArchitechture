using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contract.MessageBroker;
using Project.Infastructure.Kafka.Consumer;
using Project.Infastructure.Kafka.Producer;

namespace Project.Infastructure.Kafka
{
    public static class KafkaFactory
    {
        public static IServiceCollection UseMessageBrokerFactory(this IServiceCollection services)
        {
            //Open Generic Registration (Dấu <> nghĩa là Bất kỳ kiểu T nào cũng được)
            services.AddSingleton(typeof(IMessageConsumer<>), typeof(KafkaConsumer<>));
            services.AddSingleton(typeof(IMessageProducer<,>), typeof(KafkaProducer<,>));
            services.AddSingleton(typeof(IDeserializer<>), typeof(JsonDeserializer<>));
            services.AddSingleton(typeof(ISerializer<>), typeof(JsonSerializer<>));
            return services;
        }
    }
}
