using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public interface IUniversityRepository
    {
        IQueryable<DestSpeciality> DestSpecialityGetList();
        Task UpdateInterestedStudentsCountAsync(int id, bool increment);
    }
}
