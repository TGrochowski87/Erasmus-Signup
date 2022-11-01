using FluentResults;
using Microsoft.AspNetCore.Mvc;
using PlanApi.Models;
using PlanApi.Service;

namespace PlanApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class PlanController : Controller
    {
        private IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(planService.Example());
        }
    }
}
