using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiApplication.Filters
{
    public enum AuthorizedFor { Read, Write };

    public class AuthorizedTokenAttribute : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        AuthorizedFor _allowedClaim;

        public AuthorizedTokenAttribute(AuthorizedFor allowedClaim)
        {
            _allowedClaim = allowedClaim;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            
            if (claims == null || 
                !claims.Any() || 
                !context.HttpContext.User.Identity.IsAuthenticated ||
                claims.Where(l => l.Type == ClaimTypes.Role && l.Value == _allowedClaim.ToString()).Count() == 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
