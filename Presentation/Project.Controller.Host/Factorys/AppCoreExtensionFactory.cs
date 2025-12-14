using Project.Application;
using Project.Domain.Services;
using Project.Host.Base.Lazyloads;

namespace Project.Controller.Host.Factorys
{
    public static class AppCoreExtensionFactory
    {
        public static IServiceCollection AddLazyloadFactory(this IServiceCollection service)
        {
            return service.AddScoped<ILazyloadProvider, LazyloadProvider>();
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