using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contract.Services.Auths;
using Project.Application.Services.Auths;

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
    }
}
