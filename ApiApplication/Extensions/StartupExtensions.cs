using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class StartupExtensions
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);

            services.AddAuthentication();

            services.AddAutoMapper();

            services.AddFluentValidation();

            services.AddSwaggerGen();

            services.RegisterApplicationRepositories();

            services.RegisterApplicationServices();

        }
    }
}
