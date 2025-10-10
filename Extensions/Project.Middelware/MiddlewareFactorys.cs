
using Project.Middelware.Middlewares;

namespace Project.Middelware
{
    public static class MiddlewareFactorys
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenMiddleware>();
        }
    }
}
