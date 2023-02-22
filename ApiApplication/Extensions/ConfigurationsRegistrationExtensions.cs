using ApiApplication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class ConfigurationsRegistrationExtensions
    {
        internal static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<ImdbAPIConfig>(configuration.GetSection("ImdbAPI"));
    }
}
