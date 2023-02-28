using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.DomainService.Imdb;
using Lodgify.Cinema.DomainService.Notification;
using Lodgify.Cinema.Infrastructure.Data.Repositorie;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIocBusinessDependencies(this IServiceCollection services)
        {
            var projectEnvinronmentConfiguration = services.BuildServiceProvider().GetService<IProjectEnvinronmentConfiguration>();


            services.AddScoped<IShowtimesRepository, ShowtimesRepository>()
                    .AddScoped<IDomainNotification, DomainNotification>()
                    .AddScoped<IImdbIdTranslatorService, ImdbIdTranslatorService>()
                    .AddScoped<IImdbStatusService, ImdbStatusService>()
                    .AddSingleton<IImdbStatus, ImdbStatus>(c => SingletonImdbStatus)
                    .AddHttpClient<IImdbRepository, ImdbRepository>()
                      .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                     .ConfigureHttpClient(client => ConfigureHttpClienteForImdbAccess(client, projectEnvinronmentConfiguration));

            return services;
        }

        private static void ConfigureHttpClienteForImdbAccess(HttpClient client, IProjectEnvinronmentConfiguration projectEnvinronmentConfiguration)
        {
            client.BaseAddress = new Uri(projectEnvinronmentConfiguration.ExternalApi_Imdb_BaseUri);

            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", projectEnvinronmentConfiguration.ExternalApi_Imdb_X_RapidAPI_Key);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", projectEnvinronmentConfiguration.ExternalApi_Imdb_X_RapidAPI_Host);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static ImdbStatus SingletonImdbStatus { get; set; } = new ImdbStatus(false, null);
    }
}