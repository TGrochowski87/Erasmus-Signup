using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<University>> GetListAsync();
    }
}
