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
        Task<RecommendedDestination> GetRecommendedDestinations(UserJWT userJWT);
        Task<RecommendedDestination> GetRecommendedByStudentsDestinations(UserJWT userJWT);
        Task<DestinationResult> GetListForUserAsync(UserDestinationCriteria criteria);
    }
}
