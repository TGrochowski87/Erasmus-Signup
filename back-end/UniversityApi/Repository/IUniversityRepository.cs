using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<DestSpeciality>> GetListAsync();
        Task<University> GetAsync(short destId);
    }
}
