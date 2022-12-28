using OpinionApi.Models;

namespace OpinionApi.Service
{
    public interface IOpinionService
    {
        Task<long> CreateAsync(OpinionCreateVM opinion, long userId);
        Task<bool> DeleteAsync(long id);
        Task EditAsync(OpinionEditVM opinion, long id);
        Task<OpinionResult> GetListAsync(OpinionCriteria criteria, long? userId);
    }
}
