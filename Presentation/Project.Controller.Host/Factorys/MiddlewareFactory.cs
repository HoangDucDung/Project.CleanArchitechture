

using Project.Host.Base.Middlewares;

namespace Project.Controller.Host.Factorys
{
    public static class MiddlewareFactory
    {
        public static IApplicationBuilder MiddlewareRegistration(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AuthenMiddleware>();
            builder.UseMiddleware<ApplicationMiddleware>();

            return builder;
        }

    }
}
