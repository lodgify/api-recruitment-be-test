using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace ApiApplication.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cinema  API",
                    Version = "v1",
                });

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = @"Put ApiKey here",
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKey"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                              },
                              Scheme = "ApiKey",
                              Name = "ApiKey",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                  });
            });
        }
    }
}
