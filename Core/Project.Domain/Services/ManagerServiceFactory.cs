using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Services.Auths;

namespace Project.Domain.Services
{
    public static class ManagerServiceFactory
    {
        public static IServiceCollection AddManagerServiceFactory(this IServiceCollection services)
        {
            // Register domain services here
            services.AddScoped<ITokenManager, TokenManager>();
            return services;
        }
    }
}