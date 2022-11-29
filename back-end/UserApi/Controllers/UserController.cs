using FluentResults;
using Microsoft.AspNetCore.Mvc;

using UserApi.Models;
using UserApi.Service;
using UserApi.Common;
using UserApi.Utilities;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;

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

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet("current")]
        public IActionResult SessionLogin(string acces_token, string acces_token_secret)
        {
            HttpResponseMessage responseMessage = userService.GetCurrentUser(acces_token, acces_token_secret, authorisedService);
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                JObject querry = JObject.Parse(result);
                /*{{
 x "id": "438993",
 x "last_name": "Kwaśniak",
 x "first_name": "Sebastian",
 x "middle_names": "Artur",
 x "titles": {
 x   "before": null,
 x   "after": null
 x },
 x "student_status": 2,
 x "staff_status": 0,
 x "sex": "M",
  "email": null,
  "student_number": null,
x  "photo_urls": {
x    "50x50": "https://apps.usos.pwr.edu.pl/res/up/50x50/438993-m-73faa90855e8180da64f2e8095cc3172295b4664904fee1fee30a9f43a0e5c75.jpg",
x    "400x500": "https://apps.usos.pwr.edu.pl/res/up/400x500/438993-m-24136937755f77d6e53c96c40be9c0341441a83cbff86b1d0b14f4711cb06247.jpg"
x  }
}}*/
                if (querry.Count >= 3)
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
                        querry["photo_urls"]!["50x50"]!.ToString(),
                        querry["photo_urls"]!["400x500"]!.ToString(),
                        querry["student_number"]!.ToString()

                    ));
                }
                return BadRequest("Authorised service error: crucial elements not found");
            }
            return BadRequest("Authorised service error: " + responseMessage.ReasonPhrase);

        }

    }
}
