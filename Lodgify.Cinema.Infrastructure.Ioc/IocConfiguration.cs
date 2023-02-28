using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
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
        public static IServiceCollection ConfigureIocBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShowtimesRepository, ShowtimesRepository>()
                    .AddScoped<IDomainNotification, DomainNotification>()
                    .AddHttpClient<IImdbRepository, ImdbRepository>()
                      .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                     .ConfigureHttpClient(client => ConfigureHttpClienteForImdbAccess(client, configuration));

            return services;

        }

        private static void ConfigureHttpClienteForImdbAccess(HttpClient client, IConfiguration configuration)
        {
            
        }

    }
}