using Microsoft.AspNetCore.Authentication;

namespace ApiApplication.Auth
{
    public class CustomAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public const string AuthenticationScheme = "CustomAuthentication";
        
        public class Policies
        {
            public const string ReadOnly = "ReadOnlyPolicy";
            public const string Write = "WritePolicy";
        }
    }
}