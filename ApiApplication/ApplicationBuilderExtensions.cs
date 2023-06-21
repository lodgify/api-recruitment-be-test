using Microsoft.AspNetCore.Builder;

namespace ApiApplication
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}
