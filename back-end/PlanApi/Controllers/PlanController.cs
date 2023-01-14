using FluentResults;
using Microsoft.AspNetCore.Mvc;
using PlanApi.Attributes;
using PlanApi.Models;
using PlanApi.Service;

namespace PlanApi.Controllers
{
    [ApiController]
    [Route("plan")]
    public class PlanController : Controller, IPlanController
    {
        private readonly IPlanService planService;
        public UserJWT? UserToken { get; set; }

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }

        [AuthorizeUser]
        [HttpGet("user_plan")]
        public async Task<UserPlanGetVM> GetUserPlan()
        {
            return await planService.GetUserPlanAsync(GetUserId());
        }

        [AuthorizeUser]
        [HttpPost("user_plan")]
        public async Task CreateUserPlan([FromBody] UserPlanEditVM plan)
        {
            await planService.CreateUserPlanAsync(GetUserId(), plan);
        }

        [AuthorizeUser]
        [HttpGet("plans")]
        public async Task<IEnumerable<PlanGetVM>> GetPlans()
        {
            var id = GetUserId();
            return await planService.GetPlansAsync(id, UserToken.Token);
        }

        [AuthorizeUser]
        [HttpPost("plan")]
        public async Task<int> CreatePlan([FromBody] PlanCreateVM plan)
        {
            return await planService.CreatePlanAsync(plan, GetUserId());
        }

        [AuthorizeUser]
        [HttpPut("plan/{id}")]
        public async Task UpdatePlan(int id, [FromBody] PlanCreateVM plan)
        {
            await planService.UpdatePlanAsync(id, plan, GetUserId());
        }

        [AuthorizeUser]
        [HttpDelete("plan/{id}")]
        public async Task DeletePlan(int id)
        {
            await planService.DeletePlanAsync(id);
        }
        
        private long GetUserId()
        {
            if (UserToken == null)
            {
                throw new BadHttpRequestException("UserToken is null");
            }

            return long.Parse(UserToken.UserId);
        }
    }
}
