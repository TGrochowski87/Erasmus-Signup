using UniversityApi.Models;

namespace UniversityApi.Service
{
    public interface IUniversityService
    {
        ExampleModel Example();
        Task<IEnumerable<UniversityVM>> GetListAsync();
    }
}
