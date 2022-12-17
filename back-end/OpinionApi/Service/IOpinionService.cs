using OpinionApi.Models;

namespace OpinionApi.Service
{
    public interface IOpinionService
    {
        Task<int> CreateAsync(OpinionCreateVM opinion, int userId);
        Task<bool> DeleteAsync(int id);
        Task EditAsync(OpinionEditVM opinion, int id);
        Task<OpinionResult> GetListAsync(OpinionCriteria criteria, int? userId);
    }
}
