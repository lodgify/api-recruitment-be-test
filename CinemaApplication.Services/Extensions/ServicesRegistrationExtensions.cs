using CinemaApplication.DAL.Extensions;
using CinemaApplication.Services.Abstractions;
using CinemaApplication.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaApplication.Services.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddDAL()
                .RegisterServices()
                .AddHttpClient("default", client => { });

            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IImdbService, ImdbService>();
            services.AddScoped<IShowtimeService, ShowtimeService>();
            
            return services;
        }
    }
}
