using System.Diagnostics;
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
        public async Task<RecommendedDestination> GetRecommendedDestinations()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = await universityService.GetRecommendedDestinations(UserToken);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            return response;
        }

        [AuthorizeUser]
        [HttpGet("universities-recommended-by-students")]
        public async Task<RecommendedDestination> GetRecommendedByStudentsDestinations()
        {
            return await universityService.GetRecommendedByStudentsDestinations(UserToken);
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

        [HttpGet("universities/users")]
        public async Task<DestinationResult> GetListForUser([FromQuery] UserDestinationCriteria criteria)
        {
            return await universityService.GetListForUserAsync(criteria);
        }
    }
}
