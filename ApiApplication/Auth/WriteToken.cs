using Microsoft.AspNetCore.Authorization;

namespace ApiApplication.Auth
{
    public class WriteToken : IAuthorizationRequirement
    {
        public WriteToken(string writeTokenValue, string nameIdentifierValue)
        {
            WriteTokenValue = writeTokenValue;
            NameIdentifier = nameIdentifierValue;
        }

        public string WriteTokenValue { get; }

        public string NameIdentifier { get; set; }
    }
}
