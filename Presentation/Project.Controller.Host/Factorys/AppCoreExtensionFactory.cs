using Project.Application;
using Project.Domain.Services;
using Project.Libs.DependencyInjection;

namespace Project.Controller.Host.Factorys
{
    public static class AppCoreExtensionFactory
    {
        public static IServiceCollection AddLazyloadFactory(this IServiceCollection service)
        {
            service.AddScoped<ICachedServiceProviderBase, CachedServiceProviderBase>();
            service.AddScoped<ILazyloadProvider, LazyloadProvider>();
            return service;
        }

        public static IServiceCollection UseAppAuthenExtensionFactory(this IServiceCollection services)
        {
            services.UseAppAuthenFactory();
            return services;
        }

        public static IServiceCollection UserDomainManagerServiceFactory(this IServiceCollection services)
        {
            services.AddManagerServiceFactory();
            return services;
        }
    }
}