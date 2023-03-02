using ApiApplication.Core.Filters;
using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.DomainService.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class FilterConfiguration
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ErrorFilterAttribute>()
                    .AddScoped<PaginationFilterAttribute>()
                    .AddScoped<MetricsFilterAttribute>()
                    .AddScoped<IPaginatedRequest, PaginatedRequest>(c => new PaginatedRequest(0, 0));

            PaginationFilterAttribute.EnableOptionalPagination = services.BuildServiceProvider().GetService<IProjectEnvinronmentConfiguration>().Application_EnableOptionalPagination;

            return services;
        }

    }
}
