using CinemaApplication.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaApplication.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDAL(this IServiceCollection services)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            services.AddTransient<IShowtimeRepository, ShowtimeRepository>();
            return services;
        }
    }
}
