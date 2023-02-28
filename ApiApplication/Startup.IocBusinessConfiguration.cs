using ApiApplication.Application.Command;
using ApiApplication.Application.Querie;
using ApiApplication.Auth;
using Lodgify.Cinema.Infrastructure.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class IocBusinessConfiguration
    {
        public static IServiceCollection ConfigureBusinessDependencies(this IServiceCollection services) =>
            services.ConfigureIocBusinessDependencies()
                      .AddScoped<IAddShowTimeCommandHandler, AddShowTimeCommandHandler>()
                      .AddScoped<IUpdateShowTimeCommandHandler, UpdateShowTimeCommandHandler>()
                      .AddScoped<IGetShowTimeQueryHandler, GetShowTimeQueryHandler>()
                      .AddScoped<IDeleteShowTimeCommandHandler, DeleteShowTimeCommandHandler>()
                      .AddScoped<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
    }
}