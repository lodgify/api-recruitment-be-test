using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.DomainService.Notification;
using Lodgify.Cinema.DomainService.Imdb;
using Lodgify.Cinema.Infrastructure.Data.Repositorie;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIocBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShowtimesRepository, ShowtimesRepository>()
                    .AddScoped<IDomainNotification, DomainNotification>()
                    .AddScoped<IImdbIdTranslatorService, ImdbIdTranslatorService>()
                    .AddScoped<IImdbStatusService, ImdbStatusService>()
                    .AddSingleton<IImdbStatus, ImdbStatus>(c => SingletonImdbStatus)
                    .AddHttpClient<IImdbRepository, ImdbRepository>()
                      .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                     .ConfigureHttpClient(client => ConfigureHttpClienteForImdbAccess(client, configuration));

            return services;
        }

        private static void ConfigureHttpClienteForImdbAccess(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("ExternalApi:Imdb:BaseUri"));

            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configuration.GetValue<string>("ExternalApi:Imdb:X-RapidAPI-Key"));
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", configuration.GetValue<string>("X-RapidAPI-Host"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static ImdbStatus SingletonImdbStatus { get; set; } = new ImdbStatus(false, null);
    }
}