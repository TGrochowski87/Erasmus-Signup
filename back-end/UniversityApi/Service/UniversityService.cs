using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApi.Service
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }

        public async Task<IEnumerable<UniversityVM>> GetListAsync()
        {
            var list = await _universityRepository.GetListAsync();
            return list.Select(x => new UniversityVM(x));
        }
    }
}
