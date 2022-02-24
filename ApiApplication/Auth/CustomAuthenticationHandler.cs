using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ApiApplication.Auth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationSchemeOptions>
    {
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
                var principal = _tokenService.Read(apiKey);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, CustomAuthenticationSchemeOptions.AuthenticationScheme)));
            }
            catch (System.Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex));
            }
        }
    }
}
