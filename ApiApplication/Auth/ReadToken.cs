using Microsoft.AspNetCore.Authorization;

namespace ApiApplication.Auth
{
    public class ReadToken : IAuthorizationRequirement
    {
        public ReadToken(string readTokenValue, string nameIdentifierValue)
        {
            ReadTokenValue = readTokenValue;
            NameIdentifier = nameIdentifierValue;
        }

        public string ReadTokenValue { get; }

        public string NameIdentifier { get; set; }
    }
}
