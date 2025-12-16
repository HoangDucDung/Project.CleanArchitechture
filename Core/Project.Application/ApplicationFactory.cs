using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contract.MessageBroker;
using Project.Application.Contract.Services.Auths;
using Project.Application.Services.Auths;
using Project.Infastructure.Kafka.Consumer;

namespace Project.Application
{
    public static class ApplicationFactory
    {
        public static IServiceCollection AddApplicationFactory(this IServiceCollection services)
        {
            // Register application services here
            return services;
        }

        /// <summary>
        /// Sử dụng factory cho Authen
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseAppAuthenFactory(this IServiceCollection services)
        {
            // Configure application middleware here if needed
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        public static IServiceCollection UseMessageBrokerFactory(this IServiceCollection services)
        {
            //Open Generic Registration (Dấu <> nghĩa là Bất kỳ kiểu T nào cũng được)
            services.AddSingleton(typeof(IMessageConsumer<>), typeof(KafkaConsumer<>));
            services.AddSingleton(typeof(IDeserializer<>), typeof(JsonDeserializer<>));
            return services;
        }
    }
}
