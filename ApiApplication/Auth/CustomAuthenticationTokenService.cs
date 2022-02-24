using System.Collections.Generic;
using System.Security.Claims;

namespace ApiApplication.Auth
{
    public class CustomAuthenticationTokenService : ICustomAuthenticationTokenService
    {
        public ClaimsPrincipal Read(string value)
        {
            try
            {
                var decodedBytes = System.Convert.FromBase64String(value);
                var decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);

                var splitedData = decodedString.Split(new char[] { '|' });

                return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, splitedData[0]),
                    new Claim(ClaimTypes.Role, splitedData[1]),
                }, CustomAuthenticationSchemeOptions.AuthenticationScheme));
            }
            catch (System.Exception ex)
            {
                throw new ReadTokenException(value, ex);
            }            
        }
    }
}
