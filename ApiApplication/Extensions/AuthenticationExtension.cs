using ApiApplication.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });
        }
    }
}
