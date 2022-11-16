using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(userService.Example());
        }

        [HttpGet("oauth_url")]
        public ActionResult<OAuthUrlModel> RequestAuthUrl(string callbackPath = "oob")
        {
            return new ActionResult<OAuthUrlModel>(userService.OAuthUrl(callbackPath));
        }
    }
}
