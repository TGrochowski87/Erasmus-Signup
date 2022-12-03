using Microsoft.AspNetCore.Mvc;
using UserApi.Service;

using UserApi.Models;
using UserApi.Service;
using UserApi.Common;
using UserApi.Utilities;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace UserApi.Controllers
{

    [ApiController]
    [Route("oauth")]
    public class OAuthController : Controller
    {
        private IUserService userService;
        private IAuthorizedService authorizedService;

        public OAuthController(IUserService userService, IAuthorizedService authorizedService)
        {
            this.userService = userService;
            this.authorizedService = authorizedService;
        }

        [HttpGet("oauth_url")]
        public IActionResult RequestOAuthUrl(string callbackPath = "oob")
        {
            HttpResponseMessage responseMessage = authorizedService.GetOAuthUrl(callbackPath);
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                NameValueCollection querry = System.Web.HttpUtility.ParseQueryString(result);
                string oauth_token = !String.IsNullOrEmpty(querry["oauth_token"]) ? querry["oauth_token"]!.ToString() : "";
                string oauth_token_secret = !String.IsNullOrEmpty(querry["oauth_token_secret"]) ? querry["oauth_token_secret"]!.ToString() : "";
                if (!String.IsNullOrWhiteSpace(oauth_token) && !String.IsNullOrWhiteSpace(oauth_token_secret))
                {
                    return Ok(new OAuthUrlResponseModel(Secrets.OAuthHostUrl + Secrets.OAuthAuthMethod + "?oauth_token=" + oauth_token, oauth_token_secret));
                }
                return BadRequest("Authorized service error: authorized service did not provided crucial elements");
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        }

        [HttpGet("access_token")]
        public IActionResult SessionLogin(string oauth_token, string oauth_verifier, string oauth_token_secret)
        {
            HttpResponseMessage responseMessage = authorizedService.GetAccessToken(oauth_token, oauth_verifier, oauth_token_secret);
            if (responseMessage.IsSuccessStatusCode)
            {
                string userApiToken = "";
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                NameValueCollection querry = System.Web.HttpUtility.ParseQueryString(result);
                string oauth_token_access = !String.IsNullOrEmpty(querry["oauth_token"]) ? querry["oauth_token"]!.ToString() : "";
                string oauth_token_access_secret = !String.IsNullOrEmpty(querry["oauth_token_secret"]) ? querry["oauth_token_secret"]!.ToString() : "";
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
                            userApiToken = OAuthTool.GenerateToken(userId, oauth_token_access);
                        }
                    }

                    return Ok(new OAuthAccessTokenResponseModel(oauth_token_access, oauth_token_access_secret, userApiToken));
                }
                return BadRequest("Authorized service error: crucial elements not found");



            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);

        }

        [HttpPost("revoke_token")]
        public IActionResult SessionLogin(string oauth_token, string oauth_token_secret)
        {
            HttpResponseMessage responseMessage = authorizedService.PostRevokeToken(oauth_token, oauth_token_secret);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);

        }
    }
}
