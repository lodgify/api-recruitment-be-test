using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
