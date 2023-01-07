using PlanApi.DbModels;
using PlanApi.Models;

namespace PlanApi.Service
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanGetVM>> GetPlansAsync(long studentId, string accessToken);
        Task<int> CreatePlanAsync(PlanCreateVM plan, long studentId);
        Task UpdatePlanAsync(int planId, PlanCreateVM plan, long studentId);
        Task DeletePlanAsync(int planId);
        Task<UserPlanGetVM> GetUserPlanAsync(long studentId);
        Task CreateUserPlanAsync(long studentId, UserPlanEditVM plan);
    }
}
