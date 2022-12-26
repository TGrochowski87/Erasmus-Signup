using OpinionApi.DbModels;

namespace OpinionApi.Repository
{
    public interface IOpinionRepository
    {
        Task<long> CreateAsync(Opinion opinion);
        Task<bool> DeleteAsync(long id);
        Task EditAsync(Opinion opinion);
        Task<IEnumerable<Opinion>> GetListAsync(long specId);
    }
}
