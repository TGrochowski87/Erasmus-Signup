using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Models;
using UniversityApi.Service;

namespace UniversityApi.Controllers
{
    [ApiController]
    public class UniversityController : Controller
    {
        private readonly IUniversityService universityService;
        private readonly IPublishEndpoint publishEndpoint;

        public UniversityController(IUniversityService universityService,
            IPublishEndpoint publishEndpoint)
        {
            this.universityService = universityService;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(universityService.Example());
        }

        [HttpGet("universities")]
        public Result<IEnumerable<UniversityVM>> GetList()
        {
            return Result.Ok(universityService.GetList());
        }
    }
}
