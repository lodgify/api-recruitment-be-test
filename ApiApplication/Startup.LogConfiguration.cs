using Lodgify.Cinema.Domain.Contract.Log;
using Lodgify.Cinema.DomainService.Log;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class LogConfiguration
    {
        public static IServiceCollection ConfigureNotificationAndLog(this IServiceCollection services) =>
             services.AddSingleton<ILodgifyLogService, LodgifyLogService>();
    }
}