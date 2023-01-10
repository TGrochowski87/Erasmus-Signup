using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using UserApi.Models;
using Microsoft.IdentityModel.Logging;

namespace UserApi.Utilities
{
    public static class OAuthTool
    {
        public static Random random = new Random();

        public static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        /// <summary>
        /// Helper function to compute a hash value
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
        /// <param name="data">The data to hash</param>
        /// <returns>a Base64 string of the hash value</returns>
        public static string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public static string UrlEncode(string value)
        {
            StringBuilder result = new StringBuilder();
            Encoding enc = new UTF8Encoding(false, true);
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    foreach (var byt in enc.GetBytes(symbol.ToString()))
                        result.Append('%' + String.Format("{0:X2}", byt));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return random.Next(123400, 9999999).ToString();
        }

        /// <summary>
        /// Generate the signature
        /// </summary>
        /// <returns></returns>
        public static string GenerateSignature(string httpMethod, string methodUrl, string methodParams, string tokenSecret)
        {
            string signatureBase = httpMethod.ToUpper() + "&" + UrlEncode(methodUrl) + "&" + UrlEncode(methodParams);
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(Secrets.OAuthApiConsumerSecret), string.IsNullOrWhiteSpace(tokenSecret) ? "" : UrlEncode(tokenSecret)));
            return ComputeHash(hmacsha1, signatureBase);

        }

        public static string GenerateToken(string userId, string accessToken, string accessTokenSecret)
        {
            var claims = new[] {
                        new Claim("UserId", userId),
                        new Claim("OAuthAccessToken", accessToken),
                        new Claim("OAuthAccessTokenSecret", accessTokenSecret),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.JwtKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                  "JWTAuthenticationServer",
                  "JWTServicePostmanClient",
                  claims,
                  expires: DateTime.UtcNow.AddHours(2),
                  signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

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

        public static HttpResponseMessage CallAuthorizedService(
            string method,
            List<KeyValuePair<string, string>> urlParams,
            bool useOAuth = false,
            string oauth_token_secret = ""
        )
        {
            string url = Secrets.OAuthHostUrl + method;
            string nonce = useOAuth ? OAuthTool.GenerateNonce() : String.Empty;
            string timestamp = useOAuth ? OAuthTool.GenerateTimeStamp() : String.Empty;
            if (useOAuth)
            {
                urlParams.Add(new KeyValuePair<string, string>("oauth_consumer_key", Secrets.OAuthApiConsumerKey));
                urlParams.Add(new KeyValuePair<string, string>("oauth_nonce", nonce));
                urlParams.Add(new KeyValuePair<string, string>("oauth_signature_method", "HMAC-SHA1"));
                urlParams.Add(new KeyValuePair<string, string>("oauth_timestamp", timestamp));
                urlParams.Add(new KeyValuePair<string, string>("oauth_version", "1.0"));
            }
            string paramsString = String.Empty;
            if (urlParams.Any())
            {
                urlParams.Sort((x, y) => (String.Compare(x.Key, y.Key)));
                paramsString += OAuthTool.UrlEncode(urlParams[0].Key) + "=" + OAuthTool.UrlEncode(urlParams[0].Value);
                for (int i = 1; i < urlParams.Count; i++)
                {
                    paramsString += "&" + OAuthTool.UrlEncode(urlParams[i].Key) + "=" + OAuthTool.UrlEncode(urlParams[i].Value);
                }
            }

            if (useOAuth)
            {
                string oauth_signature = OAuthTool.GenerateSignature("GET", url, paramsString, oauth_token_secret);
                paramsString += "&oauth_signature=" + HttpUtility.UrlEncode(oauth_signature);
            }

            var client = new HttpClient
            {
                Timeout = new TimeSpan(0, 2, 0)
            };
            return client.GetAsync(url + "?" + paramsString).Result;
        }
    }
}
