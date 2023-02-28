using Lodgify.Cinema.Domain.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ApiApplication.Auth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationSchemeOptions>
    {
        private const string READ_ONLY_TOKEN = "MTIzNHxSZWFk";
        private const string WRITE_TOKEN = "Nzg5NHxXcml0ZQ==";
        private readonly ICustomAuthenticationTokenService _tokenService;

        public CustomAuthenticationHandler(
            IOptionsMonitor<CustomAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticationTokenService tokenService) : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {            
            try
            {
                var apiKey = Context.Request.Headers["ApiKey"];
                Validate(apiKey);
                var principal = _tokenService.Read(apiKey);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, CustomAuthenticationSchemeOptions.AuthenticationScheme)));
            }
            catch (System.Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex));
            }
        }

        private void Validate(string apiKey)
        {
            bool isGet = Context.Request.Method == HttpMethod.Get.Method;

            if (isGet && string.Compare(apiKey, READ_ONLY_TOKEN) != 0)
                throw new System.Exception(BusinessMessage.InvalidToken);
            else if(!isGet && string.Compare(apiKey, WRITE_TOKEN) != 0)
                throw new System.Exception(BusinessMessage.InvalidToken);
        }
    }
}