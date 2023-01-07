using PlanApi.DbModels;
using PlanApi.Models;

namespace PlanApi.Repository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetPlansAsync(long studentId);
        Task<int> CreatePlanAsync(Plan plan);
        Task UpdatePlanAsync(Plan plan);
        Task DeletePlanAsync(int id);
        Task UpdatePlanSubjects(int planId, IEnumerable<Subject> subjects);
        Task<IEnumerable<HomeSubject>> GetUserPlanAsync(long studentId);
        Task CreateUserPlanAsync(long studentId,
            IEnumerable<HomeSubject> subjects);
    }
}
