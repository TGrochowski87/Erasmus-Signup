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
        [HttpGet("authorized_ping")]
        public IActionResult AuthorizeTest()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }
            return Ok("authorized pong");
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
                JObject jUser = JObject.Parse(result);

                if (jUser.Count > 0)
                {
                    return Ok(new User(
                        Convert.ToInt64(jUser["id"]!.ToString()),
                        jUser["first_name"]!.ToString(),
                        jUser["middle_names"]!.ToString(),
                        jUser["last_name"]!.ToString(),
                        jUser["sex"]!.ToString()[0],
                        jUser["titles"]!["before"]!.ToString(),
                        jUser["titles"]!["after"]!.ToString(),
                        Convert.ToInt16(jUser["student_status"]!.ToString()),
                        Convert.ToInt16(jUser["staff_status"]!.ToString()),
                        jUser["email"]!.ToString(),
                        jUser["photo_urls"]!["50x50"]!.ToString(),
                        jUser["photo_urls"]!["400x500"]!.ToString(),
                        jUser["student_number"]!.ToString()
                    ));
                }
                return BadRequest("Authorized service error: crucial elements not found");
            }
            return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        }

        //[AuthorizeUser]
        //[HttpGet("student_programmes")]
        //public IActionResult GetStudentProgrammes()
        //{
        //    if (UserToken == null)
        //    {
        //        return Unauthorized();
        //    }
        //    HttpResponseMessage responseMessage = userService.GetStudentProgrammes(UserToken.UserId, UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        string result = responseMessage.Content.ReadAsStringAsync().Result;
        //        //JObject jUserProgrammes = JObject.Parse("{\"programmes\":" + result + "}");
        //        JArray jUserProgrammes = JArray.Parse(result);
        //        return Ok("pong");
        //    }
        //    return BadRequest("Authorized service error: " + responseMessage.ReasonPhrase);
        //}
    }
}
