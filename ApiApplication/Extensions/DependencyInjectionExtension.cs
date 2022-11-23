using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Services.IMDB;
using ApiApplication.Services.Showtimes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();

            services.AddTransient<IShowtimeService, ShowtimeService>();
            services.AddTransient<IImdbService, ImdbService>();

        }

        public static void RegisterApplicationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();

        }

        public static void RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            //var jwtSettingsSection = configuration.GetSection("JwtSettings");
            //services.Configure<JwtSettings>(jwtSettingsSection);

        }


    }
}
