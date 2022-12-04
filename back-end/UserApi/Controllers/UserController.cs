using FluentResults;
using Microsoft.AspNetCore.Mvc;

using UserApi.Models;
using UserApi.Service;
using UserApi.Common;
using UserApi.Utilities;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using UserApi.Attributes;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private IUserService userService;
        private IAuthorizedService authorizedService;

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

        [HttpGet("current")]
        public IActionResult SessionLogin(string access_token, string access_token_secret)
        {
            HttpResponseMessage responseMessage = userService.GetCurrentUser(access_token, access_token_secret);
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
        [HttpGet("test/loin_test")]
        public IActionResult Test()
        {
            return Ok();
        }

    }
}
