using FluentResults;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Attributes;
using UniversityApi.Models;
using UniversityApi.Service;

namespace UniversityApi.Controllers
{
    [ApiController]
    public class UniversityController : Controller, IUniversityApiController
    {
        private readonly IUniversityService universityService;
        private readonly IPublishEndpoint publishEndpoint;

        public UserJWT? UserToken { get; set; }


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

        [AuthorizeUser]
        [HttpGet("universities-recommended")]
        public async Task<IEnumerable<DestinationVM>> GetRecommendedDestinations()
        {
            return await universityService.GetRecommendedDestinations();
        }

        [AuthorizeUser]
        [HttpGet("universities-recommended-by-students")]
        public async Task<IEnumerable<DestinationVM>> GetRecommendedByStudentsDestinations()
        {
            return await universityService.GetRecommendedByStudentsDestinations();
        }

        [HttpGet("countries")]
        public ActionResult<IEnumerable<string>> GetCountries()
        {
            return Ok(universityService.GetCountries());
        }

        [HttpGet("study-domains")]
        public async Task<IEnumerable<StudyDomainVM>> GetStudyDomainList()
        {
            return await universityService.GetStudyDomainListAsync();
        }

        [HttpGet("study-areas")]
        public async Task<IEnumerable<StudyAreaVM>> GetStudyAreaList()
        {
            return await universityService.GetStudyAreaListAsync();
        }

        [HttpGet("universities/{destId}")]
        public async Task<UniversityGetVM> Get(short destId)
        {
            return await universityService.GetAsync(destId);
        }
    }
}
