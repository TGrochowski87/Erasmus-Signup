using Azure.Core;
using MassTransit.Initializers;
using Newtonsoft.Json.Linq;
using PlanApi.Models;
using PlanApi.Repository;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace PlanApi.Service
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly HttpClient _httpClient;

        public PlanService(IPlanRepository planRepository, HttpClient httpClient)
        {
            _planRepository = planRepository;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PlanGetVM>> GetPlansAsync(long studentId, string bearer)
        {
            var plans = await _planRepository.GetPlansAsync(studentId);
            var result = await GetUserFirstAndLastName(bearer);

            return plans.Select(plan => new PlanGetVM(plan, result.firstName, result.lastName));
        }

        private async Task<(string firstName, string lastName)>
            GetUserFirstAndLastName(string bearer)
        {
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            var result = await _httpClient.GetAsync("user/current");
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            JObject jUser = JObject.Parse(await result.Content.ReadAsStringAsync());
            var firstName = jUser["firstName"];
            var lastName = jUser["lastName"];
            if (firstName is null || lastName is null)
            {
                throw new InvalidDataException("Missing 'firstName' and 'lastName' fields");
            }

            return (firstName.ToString(), lastName.ToString());
        }

        public async Task<int> CreatePlanAsync(PlanCreateVM plan, long studentId)
        {
            var planId = await _planRepository.CreatePlanAsync(plan.ToModel(studentId));
            var subjects = plan.Subjects.Select(x => x.ToModel(planId));
            await _planRepository.UpdatePlanSubjects(planId, subjects);

            return planId;
        }

        public async Task UpdatePlanAsync(int planId, PlanCreateVM plan, long studentId)
        {
            await _planRepository.UpdatePlanAsync(plan.ToModel(planId, studentId));
            var subjects = plan.Subjects.Select(x => x.ToModel(planId));
            await _planRepository.UpdatePlanSubjects(planId, subjects);
        }

        public async Task DeletePlanAsync(int planId)
        {
            await _planRepository.DeletePlanAsync(planId);
        }

        public async Task<UserPlanGetVM> GetUserPlanAsync(long studentId)
        {
            var subjects = (await _planRepository.GetUserPlanAsync(studentId))
                .Select(x => new HomeSubjectGetVM(x)).ToList();
            return new UserPlanGetVM(subjects);
        }

        public async Task CreateUserPlanAsync(long studentId, UserPlanEditVM plan)
        {
            await _planRepository.CreateUserPlanAsync(studentId,
                plan.Subjects.Select(x => x.ToModel(studentId)));
        }
    }
}
