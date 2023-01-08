using UniversityApi.Models;

namespace UniversityApi.Service
{
    public interface IUniversityService
    {
        Task<DestinationResult> GetListAsync(DestinationCriteria criteria);
        Task<UniversityGetVM> GetAsync(short destId);
        Task<IEnumerable<StudyDomainVM>> GetStudyDomainListAsync();
        Task<IEnumerable<StudyAreaVM>> GetStudyAreaListAsync();
        IEnumerable<string> GetCountries();
        Task<IEnumerable<DestinationVM>> GetRecommendedDestinations(int userId);
        Task<IEnumerable<DestinationVM>> GetRecommendedByStudentsDestinations();
    }
}
