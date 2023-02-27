using ApiApplication.Auth;
using ApiApplication.Database;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureBusinessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            return services;
        }
    }
}
