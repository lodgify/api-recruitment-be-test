using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace ApiApplication.Extensions
{
    public static class SwaggerRegistrationExtensions
    {
        internal static IServiceCollection AddSwager(this IServiceCollection services)
            => services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema API", Version = "v1" });

                var docsPath = $"{AppDomain.CurrentDomain.BaseDirectory}{AppDomain.CurrentDomain.FriendlyName}.xml";
                if (File.Exists(docsPath))
                {
                    options.IncludeXmlComments(docsPath);
                }
            });
    }
}
