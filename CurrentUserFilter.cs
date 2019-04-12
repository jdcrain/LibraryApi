using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using JsonApiDotNetCore.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi
{
    public class CurrentUserFilter : ActionFilterAttribute
    {
        // Create an action filter that adds the user to the current HttpContext of the request
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault(a => a.Contains("Bearer "));

            if (authHeader == null)
            {
                throw new JsonApiException(401, "Unauthorized");
            }

            var token = authHeader.Replace("Bearer ", "");

            var securityKey = httpContext.RequestServices.GetRequiredService<SymmetricSecurityKey>(); // pull in security key service from startup, which has encoded signing key 
            
            try
            {
                var userClaim = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters 
                {
                    ValidateIssuer = false, // secret key may be shared with other services in our application, so set to false
                    ValidateAudience =  false, // secret key may be shared with other services in our application, so set to false
                    ValidateLifetime = true, // verify token hasn't expired
                    ValidateIssuerSigningKey = true, // validate issuer signing key so we know the key is coming from the secret value
                    IssuerSigningKey = securityKey
                }, out var validatedToken);

                httpContext.User = userClaim;            
            }
            catch (SecurityTokenException) 
            {
                throw new JsonApiException(401, "Unauthorized");
            }
        }
    }
}