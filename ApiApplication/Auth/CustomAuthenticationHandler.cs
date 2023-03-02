using Lodgify.Cinema.Domain.Contract;
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
        private readonly ICustomAuthenticationTokenService _tokenService;
        private readonly IProjectEnvinronmentConfiguration _projectEnvinronmentConfiguration;

        public CustomAuthenticationHandler(
            IOptionsMonitor<CustomAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticationTokenService tokenService,
            IProjectEnvinronmentConfiguration projectEnvinronmentConfiguration) : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
            _projectEnvinronmentConfiguration = projectEnvinronmentConfiguration;
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

            if (isGet && string.Compare(apiKey, _projectEnvinronmentConfiguration.Auth_ReadOnlyToken) != 0)
                throw new System.Exception(BusinessMessage.InvalidToken);
            else if (!isGet && string.Compare(apiKey, _projectEnvinronmentConfiguration.Auth_WriteToken) != 0)
                throw new System.Exception(BusinessMessage.InvalidToken);
        }
    }
}