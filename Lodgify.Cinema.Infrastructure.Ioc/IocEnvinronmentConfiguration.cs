using Lodgify.Cinema.Domain.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public static class IocEnvinronmentConfiguration
    {
        public static IServiceCollection ConfigureEnvironment(this IServiceCollection services) =>
            services.AddSingleton<IProjectEnvinronmentConfiguration, ProjectEnvinronmentConfiguration>(s =>
            {
                ProjectEnvinronmentConfiguration conf = new ProjectEnvinronmentConfiguration();

                conf.Application_EnableOptionalPagination = Convert.ToBoolean(Environment.GetEnvironmentVariable("Application_EnableOptionalPagination"));
                conf.ExternalApi_Imdb_X_RapidAPI_Key = Environment.GetEnvironmentVariable("ExternalApi_Imdb_X-RapidAPI-Key");
                conf.ExternalApi_Imdb_X_RapidAPI_Host = Environment.GetEnvironmentVariable("ExternalApi_Imdb_X-RapidAPI-Host");
                conf.ExternalApi_Imdb_BaseUri = Environment.GetEnvironmentVariable("ExternalApi_Imdb_BaseUri");
                conf.Auth_ReadOnlyToken = Environment.GetEnvironmentVariable("Auth_ReadOnlyToken");
                conf.Auth_WriteToken = Environment.GetEnvironmentVariable("Auth_WriteToken");

                return conf;
            });
    }
}