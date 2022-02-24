using System.Security.Claims;

namespace ApiApplication.Auth
{
    public interface ICustomAuthenticationTokenService
    {
        ClaimsPrincipal Read(string value);
    }
}