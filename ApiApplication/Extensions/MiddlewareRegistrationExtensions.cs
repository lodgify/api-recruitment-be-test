using ApiApplication.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ApiApplication.Extensions
{
    internal static class MiddlewareRegistrationExtensions
    {
        internal static IApplicationBuilder AddMiddlewares(this IApplicationBuilder appBuilder)
        {
            appBuilder
                .UseMiddleware<BenchmarkProcessingTimeMiddleware>()
                .UseMiddleware<ErrorHandlerMiddleware>();

            return appBuilder;
        }
    }
}
