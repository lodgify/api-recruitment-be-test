using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;
using Lodgify.Cinema.Infrastructure.Ioc;
using ApiApplication.Application.Command;

namespace ApiApplication
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureBusinessDependencies(this IServiceCollection services) =>
            services.ConfigureIocBusinessDependencies()
                    .AddScoped<IAddShowTimeCommandHandler, AddShowTimeCommandHandler>()
                    .AddScoped<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
    }
}