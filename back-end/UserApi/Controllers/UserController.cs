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
        private IAuthorisedService authorisedService;

        public UserController(IUserService userService, IAuthorisedService authorisedService)
        {
            this.userService = userService;
            this.authorisedService = authorisedService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet("current")]
        public IActionResult SessionLogin(string acces_token, string acces_token_secret)
        {
            HttpResponseMessage responseMessage = userService.GetCurrentUser(acces_token, acces_token_secret, authorisedService);
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
                return BadRequest("Authorised service error: crucial elements not found");
            }
            return BadRequest("Authorised service error: " + responseMessage.ReasonPhrase);

        }

        [AuthorizeUser]
        [HttpGet("test/logowania")]
        public IActionResult Test()
        {
            return Ok();
        }

    }
}
