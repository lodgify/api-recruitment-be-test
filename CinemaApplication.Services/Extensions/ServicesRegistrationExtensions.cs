using CinemaApplication.Core.Constants;
using CinemaApplication.Core.Models;
using CinemaApplication.DAL.Extensions;
using CinemaApplication.Services.Abstractions;
using CinemaApplication.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CinemaApplication.Services.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddDAL()
                .RegisterServices()
                .RegisterHttpClient();

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IImdbService, ImdbService>();
            services.AddScoped<IShowtimeService, ShowtimeService>();

            return services;
        }

        private static IServiceCollection RegisterHttpClient(this IServiceCollection services)
        {
            var imdbConfig = services.BuildServiceProvider().GetService<IOptions<ImdbAPIConfig>>();
            services
                .AddHttpClient(ApiConstants.IMDB_HTTP_CLIENT_KEY, client =>
                {
                    client.BaseAddress = new System.Uri(imdbConfig.Value.Url);
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", imdbConfig.Value.XRapidAPIKey);
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", imdbConfig.Value.XRapidAPIHost);
                });

            return services;
        }
    }
}
