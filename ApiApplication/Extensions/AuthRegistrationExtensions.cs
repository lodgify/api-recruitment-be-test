using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace ApiApplication.Extensions
{
    public static class AuthRegistrationExtensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Read", policy => policy.RequireClaim(ClaimTypes.Role, "Read"));
                options.AddPolicy("Write", policy => policy.RequireClaim(ClaimTypes.Role, "Write"));
            });

            return services;
        }
    }
}
