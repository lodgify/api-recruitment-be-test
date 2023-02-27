using ApiApplication.Core.Filters;
using Lodgify.Cinema.Domain.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class FilterConfiguration
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services) =>
            services.AddScoped<ErrorFilterAttribute>()
                    .AddScoped<PaginationFilterAttribute>()
                    .AddScoped<IPaginatedRequest, PaginatedRequest>(c => new PaginatedRequest(0, 0));

    }
}
