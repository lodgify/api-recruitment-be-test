using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.DomainService.Notification;
using Lodgify.Cinema.Infrastructure.Data.Repositorie;
using Microsoft.Extensions.DependencyInjection;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIocBusinessDependencies(this IServiceCollection services) =>
            services.AddScoped<IShowtimesRepository, ShowtimesRepository>()
                    .AddScoped<IImdbRepository, ImdbRepository>()
                    .AddScoped<IDomainNotification, DomainNotification>();
    }
}