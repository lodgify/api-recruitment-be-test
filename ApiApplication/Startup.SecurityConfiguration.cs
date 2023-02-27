using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
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
