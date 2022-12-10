using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UserApi.Models;
using UserApi.Service;
using UserApi.Attributes;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller, IUserApiController
    {
        private IUserService userService;
        private IAuthorizedService authorizedService;
        public UserJWT? UserToken { get; set; }

        public UserController(IUserService userService, IAuthorizedService authorizedService)
        {
            this.userService = userService;
            this.authorizedService = authorizedService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [AuthorizeUser]
        [HttpGet("current")]
        public ActionResult<User> GetCurrentUser()
        {
            if(UserToken == null)
            {
                return Unauthorized();
            }
            HttpResponseMessage responseMessage = userService.GetCurrentUser(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                JObject query = JObject.Parse(result);

                if (query.Count > 0)
                {
                    return Ok(new User(
                        Convert.ToInt64(query["id"]!.ToString()),
                        query["first_name"]!.ToString(),
                        query["middle_names"]!.ToString(),
                        query["last_name"]!.ToString(),
                        query["sex"]!.ToString()[0],
                        query["titles"]!["before"]!.ToString(),
                        query["titles"]!["after"]!.ToString(),
                        Convert.ToInt16(query["student_status"]!.ToString()),
                        Convert.ToInt16(query["staff_status"]!.ToString()),
                        query["email"]!.ToString(),
                        query["photo_urls"]!["50x50"]!.ToString(),
                        query["photo_urls"]!["400x500"]!.ToString(),
                        query["student_number"]!.ToString()
                    ));
                }
                return BadRequest("Authorized service error: crucial elements not found");
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        }

        [AuthorizeUser]
        [HttpGet("test/authorize_test")]
        public IActionResult AuthorizeTest()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }
            return Ok();
        }

    }
}
