using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiApplication.Auth
{
    public class ReadTokenHandler : AuthorizationHandler<ReadToken>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadToken requirement)
        {
            if (!(context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier) && context.User.HasClaim(x => x.Type == ClaimTypes.Role)))
            {
                return Task.CompletedTask;
            }

            var nameClaim = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            var roleClaim = context.User.FindFirst(x => x.Type == ClaimTypes.Role);

            if (!nameClaim.Value.ToLower().Equals(requirement.NameIdentifier.ToLower()) && !roleClaim.Value.ToLower().Equals(requirement.ReadTokenValue.ToLower()))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
