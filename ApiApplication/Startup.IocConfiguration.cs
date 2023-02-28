using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;
using Lodgify.Cinema.Infrastructure.Ioc;
using ApiApplication.Application.Command;
using ApiApplication.Application.Querie;
using Microsoft.Extensions.Configuration;

namespace ApiApplication
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureBusinessDependencies(this IServiceCollection services, IConfiguration configuration) =>
            services.ConfigureIocBusinessDependencies(configuration)
                    .AddScoped<IAddShowTimeCommandHandler, AddShowTimeCommandHandler>()
                    .AddScoped<IGetShowTimeQueryHandler, GetShowTimeQueryHandler>()
                    .AddScoped<IDeleteShowTimeCommandHandler, DeleteShowTimeCommandHandler>()
                    .AddScoped<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
    }
}