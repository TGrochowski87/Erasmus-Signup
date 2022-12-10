using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using UserApi.Models;
using UserApi.Service;
using UserApi.Utilities;
using UserApi.Attributes;

namespace UserApi.Controllers
{

    [ApiController]
    [Route("oauth")]
    public class OAuthController : Controller,IUserApiController
    {
        private IUserService userService;
        private IAuthorizedService authorizedService;
        public UserJWT? UserToken { get; set; }

        public OAuthController(IUserService userService, IAuthorizedService authorizedService)
        {
            this.userService = userService;
            this.authorizedService = authorizedService;
        }

        [HttpGet("oauth_url")]
        public ActionResult<OAuthUrlResponseModel> RequestOAuthUrl(string callbackPath = "oob")
        {
            HttpResponseMessage responseMessage = authorizedService.GetOAuthUrl(callbackPath);
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                NameValueCollection query = System.Web.HttpUtility.ParseQueryString(result);
                string oauth_token = !String.IsNullOrEmpty(query["oauth_token"]) ? query["oauth_token"]!.ToString() : "";
                string oauth_token_secret = !String.IsNullOrEmpty(query["oauth_token_secret"]) ? query["oauth_token_secret"]!.ToString() : "";
                if (!String.IsNullOrWhiteSpace(oauth_token) && !String.IsNullOrWhiteSpace(oauth_token_secret))
                {
                    return Ok(new OAuthUrlResponseModel(Secrets.OAuthHostUrl + Secrets.OAuthAuthMethod + "?oauth_token=" + oauth_token, oauth_token_secret));
                }
                return BadRequest("Authorized service error: authorized service did not provided crucial elements");
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        }

        [HttpGet("acces_token")]
        public ActionResult<string> SessionLogin(string oauth_token, string oauth_verifier, string oauth_token_secret)
        {
            HttpResponseMessage responseMessage = authorizedService.GetAccessToken(oauth_token, oauth_verifier, oauth_token_secret);
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                NameValueCollection query = System.Web.HttpUtility.ParseQueryString(result);
                string oauth_token_access = !String.IsNullOrEmpty(query["oauth_token"]) ? query["oauth_token"]!.ToString() : "";
                string oauth_token_access_secret = !String.IsNullOrEmpty(query["oauth_token_secret"]) ? query["oauth_token_secret"]!.ToString() : "";
                if (!String.IsNullOrWhiteSpace(oauth_token_access) && !String.IsNullOrWhiteSpace(oauth_token_access_secret))
                {
                    HttpResponseMessage responseMessageUserId = userService.GetCurrentUser(oauth_token_access, oauth_token_access_secret);

                    if (responseMessageUserId.IsSuccessStatusCode)
                    {
                        string userId;
                        string resultUserId = responseMessageUserId.Content.ReadAsStringAsync().Result;
                        JObject jUserId = JObject.Parse(resultUserId);

                        if (jUserId.Count > 0)
                        {
                            userId = jUserId["id"]!.ToString();
                            return Ok(OAuthTool.GenerateToken(userId, oauth_token_access, oauth_token_access_secret));
                        }
                        return NotFound("Authorized service error: userId not found");
                    }
                    return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
                }
                return NotFound("Authorized service error: crucial elements not found");
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        }

        [AuthorizeUser]
        [HttpPost("revoke_token")]
        public IActionResult RevokeToken()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }
            HttpResponseMessage responseMessage = authorizedService.PostRevokeToken(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);

        }
    }
}
