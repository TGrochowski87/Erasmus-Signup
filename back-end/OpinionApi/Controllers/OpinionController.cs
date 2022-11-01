using FluentResults;
using Microsoft.AspNetCore.Mvc;
using OpinionApi.Models;
using OpinionApi.Service;

namespace OpinionApi.Controllers
{
    [ApiController]
    [Route("opinion")]
    public class OpinionController : Controller
    {
        private readonly IOpinionService opinionService;

        public OpinionController(IOpinionService opinionService)
        {
            this.opinionService = opinionService;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(opinionService.Example());
        }
    }
}
