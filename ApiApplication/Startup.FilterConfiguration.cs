using ApiApplication.Core.Filters;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.DomainService.Pagination;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class FilterConfiguration
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ErrorFilterAttribute>()
                    .AddScoped<PaginationFilterAttribute>()
                    .AddScoped<MetricsFilterAttribute>()
                    .AddScoped<IPaginatedRequest, PaginatedRequest>(c => new PaginatedRequest(0, 0));

            PaginationFilterAttribute.EnableOptionalPagination = configuration.GetValue<bool>("Application:EnableOptionalPagination");

            return services;
        }

    }
}
