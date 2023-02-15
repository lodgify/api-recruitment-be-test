using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions

{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
    }
}
