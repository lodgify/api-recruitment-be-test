using ApiApplication.Database;
using Lodgify.Cinema.Infrastructure.Data.Context;
using Lodgify.Cinema.Infrastructure.Data.Database;
using Lodgify.Cinema.Infrastructure.Data.Repositorie;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Lodgify.Cinema.Infrastructure.Ioc
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIocBusinessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            return services;
        }


        public static IServiceCollection ConfigureIocDbDependencies(this IServiceCollection services)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            return services;
        }

        public static void Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
            SampleData.Initialize(context);
        }
    }
}
