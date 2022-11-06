using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Models;
using UniversityApi.Service;

namespace UniversityApi.Controllers
{
    [ApiController]
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

        [HttpGet("universities")]
        public Result<IEnumerable<UniversityVM>> GetList()
        {
            return Result.Ok(universityService.GetList());
        }
    }
}
