using Microsoft.AspNetCore.Authentication;

namespace ApiApplication.Auth
{
    public class CustomAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public const string AuthenticationScheme = "CustomAuthentication";
    }
}
