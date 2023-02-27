using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ApiApplication
{
    public static class ApiDocumentationConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Lodgify Cinema Api",
                    Description = "An ASP.NET Core Web API for cinema items - By Diogo Nunes",
                });
            });

        public static void UseSwaggerAndUi(this IApplicationBuilder app) =>
            app.UseSwagger()
               .UseSwaggerUI();
    }
}