using OpinionApi.DbModels;

namespace OpinionApi.Repository
{
    public interface IOpinionRepository
    {
        Task<int> CreateAsync(Opinion opinion);
        Task<bool> DeleteAsync(int id);
        Task EditAsync(Opinion opinion);
        Task<IEnumerable<Opinion>> GetListAsync(int specId);
    }
}
