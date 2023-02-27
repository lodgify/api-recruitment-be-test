using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class DbConfiguration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            return services;
        }
    }
}
