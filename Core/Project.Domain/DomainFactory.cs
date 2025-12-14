using Microsoft.Extensions.DependencyInjection;

namespace Project.Domain
{
    public static class DomainFactory
    {
        /// <summary>
        /// Add base domain services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainBaseFactory(this IServiceCollection services)
        {
            // Register application services here
            return services;
        }

        public static IServiceCollection AddDomainBussinessFactory(this IServiceCollection services)
        {
            services.AddDomainBaseFactory();
            // Register application services here
            return services;
        }

        public static IServiceCollection AddDomainAuthenFactory(this IServiceCollection services)
        {
            services.AddDomainBaseFactory();
            // Register application services here
            return services;
        }
    }
}
