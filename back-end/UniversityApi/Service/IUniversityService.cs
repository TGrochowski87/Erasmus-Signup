using UniversityApi.Models;

namespace UniversityApi.Service
{
    public interface IUniversityService
    {
        Task<DestinationResult> GetListAsync(DestinationCriteria criteria);
        Task<UniversityGetVM> GetAsync(short destId);
    }
}
