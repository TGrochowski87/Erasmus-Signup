using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public interface IUniversityRepository
    {
        IEnumerable<University> GetList();
    }
}
