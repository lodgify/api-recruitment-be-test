using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ApiApplication.Auth
{ 
    public static class AuthorizationPolicyConfig
    {
        public static void ConfigureAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Read-Only", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var httpContext = context.Resource as HttpContext;
                        var apiKey = httpContext.Request.Headers["ApiKey"];
                        return apiKey == configuration.GetSection("ApiKey:Read").Value.Trim();
                    });
                });
                options.AddPolicy("Write", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var httpContext = context.Resource as HttpContext;
                        var apiKey = httpContext.Request.Headers["ApiKey"];
                        return apiKey == configuration.GetSection("ApiKey:Write").Value.Trim();
                    });
                });
            });
        }
    }
}