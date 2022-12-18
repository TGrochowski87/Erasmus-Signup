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

        public UniversityController(IUniversityService universityService)
        {
            this.universityService = universityService;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet("universities")]
        public async Task<DestinationResult> GetList([FromQuery] DestinationCriteria criteria)
        {
            return await universityService.GetListAsync(criteria);
        }

        [HttpGet("universities/{destId}")]
        public async Task<UniversityGetVM> Get(short destId)
        {
            return await universityService.GetAsync(destId);
        }
    }
}
