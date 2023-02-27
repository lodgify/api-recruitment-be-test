using ApiApplication.Core.Notification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiApplication
{
    public static class LogConfiguration
    {
        public static IServiceCollection ConfigureNotificationAndLog(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger<string>>()
                    .AddScoped<IDomainNotification, DomainNotification>();
            return services;
        }
    }
}