using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Models;
using UniversityApi.Service;

namespace UniversityApi.Controllers
{
    [ApiController]
    [Route("university")]
    public class UniversityController : Controller
    {
        private readonly IUniversityService universityService;

        public UniversityController(IUniversityService universityService)
        {
            this.universityService = universityService;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(universityService.Example());
        }
    }
}
