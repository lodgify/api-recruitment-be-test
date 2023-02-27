using ApiApplication.Core.Filters;
using ApiApplication.Core.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class FilterConfiguration
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ErrorFilterAttribute>();
            services.AddScoped<PaginationFilterAttribute>();
            services.AddScoped<IPaginatedRequest, PaginatedRequest>(c => new PaginatedRequest(0, 0));
            return services;
        }
    }
}
