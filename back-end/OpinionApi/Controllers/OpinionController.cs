using Microsoft.AspNetCore.Mvc;
using OpinionApi.Attributes;
using OpinionApi.Models;
using OpinionApi.Service;

namespace OpinionApi.Controllers
{
    [ApiController]
    public class OpinionController : Controller, IOpinionApiController
    {
        private readonly IOpinionService opinionService;

        public UserJWT? UserToken { get; set; }

        public OpinionController(IOpinionService opinionService)
        {
            this.opinionService = opinionService;
        }

        [AuthorizeUserOptional]
        [HttpGet("opinions")]
        public async Task<OpinionResult> GetList([FromQuery] OpinionCriteria criteria)
        {
            return await opinionService.GetListAsync(criteria, UserToken != null ? int.Parse(UserToken.UserId) : null);
        }

        [AuthorizeUser]
        [HttpPost("opinions")]
        public async Task<int> Create([FromBody] OpinionCreateVM create)
        {
            return await opinionService.CreateAsync(create, int.Parse(UserToken.UserId));
        }

        [AuthorizeUser]
        [HttpPut("opinions/{id}")]
        public async Task Edit([FromRoute]int id,[FromBody] OpinionEditVM edit)
        {
             await opinionService.EditAsync(edit, id);
        }

        [AuthorizeUser]
        [HttpDelete("opinions/{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await opinionService.DeleteAsync(id);
        }

    }
}
