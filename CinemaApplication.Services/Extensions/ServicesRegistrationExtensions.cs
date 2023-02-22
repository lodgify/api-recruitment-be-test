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
                .AddHttpClient("ImdbAPI", client => 
                {
                    client.BaseAddress = new System.Uri("https://imdb8.p.rapidapi.com/");
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "2cb6f07896mshed6c555fa39c38fp1d618fjsnae88ac9183b8");
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "imdb8.p.rapidapi.com");
                });

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
