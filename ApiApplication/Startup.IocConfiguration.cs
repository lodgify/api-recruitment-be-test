using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;
using Lodgify.Cinema.Infrastructure.Ioc;

namespace ApiApplication
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureBusinessDependencies(this IServiceCollection services) =>
            services.ConfigureIocBusinessDependencies()
                    .AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
    }
}
