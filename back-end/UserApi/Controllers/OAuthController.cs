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
        private IAuthorisedService authorisedService;

        public OAuthController(IUserService userService, IAuthorisedService authorisedService)
        {
            this.userService = userService;
            this.authorisedService = authorisedService;
        }

        [HttpGet("oauth_url")]
        public IActionResult RequestOAuthUrl(string callbackPath = "oob")
        {
            HttpResponseMessage responseMessage = authorisedService.GetOAuthUrl(callbackPath);
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
                return BadRequest("Authorised service error: authorised service did not provided crucial elements");
            }
            return BadRequest("Authorised service error: " + responseMessage.ReasonPhrase);
        }

        [HttpGet("acces_token")]
        public IActionResult SessionLogin(string oauth_token, string oauth_verifier, string oauth_token_secret)
        {
            HttpResponseMessage responseMessage = authorisedService.GetAccesToken(oauth_token, oauth_verifier, oauth_token_secret);
            if (responseMessage.IsSuccessStatusCode)
            {
                string userApiToken = "";
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                NameValueCollection querry = System.Web.HttpUtility.ParseQueryString(result);
                string oauth_token_acces = !String.IsNullOrEmpty(querry["oauth_token"]) ? querry["oauth_token"]!.ToString() : "";
                string oauth_token_acces_secret = !String.IsNullOrEmpty(querry["oauth_token_secret"]) ? querry["oauth_token_secret"]!.ToString() : "";
                if (!String.IsNullOrWhiteSpace(oauth_token_acces) && !String.IsNullOrWhiteSpace(oauth_token_acces_secret))
                {
                    HttpResponseMessage responseMessageUserId = userService.GetCurrentUser(oauth_token_acces, oauth_token_acces_secret, authorisedService);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string userId;
                        string resultUserId = responseMessage.Content.ReadAsStringAsync().Result;
                        JObject querryUserId = JObject.Parse(result);

                        if (querry.Count > 0)
                        {
                            userId = querry["id"]!.ToString();
                            userApiToken = OAuthTool.GenerateToken(userId, oauth_token_acces);
                        }
                    }

                    return Ok(new OAuthAccesTokenResponseModel(oauth_token_acces, oauth_token_acces_secret, userApiToken));
                }
                return BadRequest("Authorised service error: crucial elements not found");



            }
            return BadRequest("Authorised service error: " + responseMessage.ReasonPhrase);

        }

        [HttpPost("revoke_token")]
        public IActionResult SessionLogin(string oauth_token, string oauth_token_secret)
        {
            HttpResponseMessage responseMessage = authorisedService.PostRevokeToken(oauth_token, oauth_token_secret);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest("Authorised service error: " + responseMessage.ReasonPhrase);

        }
    }
}
