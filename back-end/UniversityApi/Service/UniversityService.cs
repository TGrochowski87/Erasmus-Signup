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

        public IEnumerable<UniversityVM> GetList()
        {
            return _universityRepository.GetList().Select(x => new UniversityVM(x));
        }
    }
}
