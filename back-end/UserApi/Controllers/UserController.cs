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
                JObject querry = JObject.Parse(result);

                if (querry.Count > 0)
                {
                    return Ok(new User(
                        Convert.ToInt64(querry["id"]!.ToString()),
                        querry["first_name"]!.ToString(),
                        querry["middle_names"]!.ToString(),
                        querry["last_name"]!.ToString(),
                        querry["sex"]!.ToString()[0],
                        querry["titles"]!["before"]!.ToString(),
                        querry["titles"]!["after"]!.ToString(),
                        Convert.ToInt16(querry["student_status"]!.ToString()),
                        Convert.ToInt16(querry["staff_status"]!.ToString()),
                        querry["email"]!.ToString(),
                        querry["photo_urls"]!["50x50"]!.ToString(),
                        querry["photo_urls"]!["400x500"]!.ToString(),
                        querry["student_number"]!.ToString()
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
