using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public interface IUniversityRepository
    {
        IQueryable<DestSpeciality> DestSpecialityGetList();
    }
}
