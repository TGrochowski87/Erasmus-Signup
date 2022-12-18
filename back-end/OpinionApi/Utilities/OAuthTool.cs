using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OpinionApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpinionApi.Utilities
{
    public class OAuthTool
    {
        public static UserJWT DecodeToken(string token)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                SecurityToken validatedToken;
                TokenValidationParameters validationParameters = new TokenValidationParameters();

                validationParameters.ValidateLifetime = true;
                validationParameters.ValidateIssuerSigningKey = true;
                validationParameters.ValidAudience = "JWTServicePostmanClient";
                validationParameters.ValidIssuer = "JWTAuthenticationServer";
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.JwtKey));

                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

                return new UserJWT(principal.FindFirst("UserId")?.Value ?? "", principal.FindFirst("OAuthAccessToken")?.Value ?? "", principal.FindFirst("OAuthAccessTokenSecret")?.Value ?? "", true);
            }
            catch
            {
                return new UserJWT(false);
            }

        }
    }
}
