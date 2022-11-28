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



        public IEnumerable<DestinationVM> DestSpecialityGetList()
        {
            return _universityRepository.DestSpecialityGetList()
                .Select(x=> new DestinationVM(x));
        }
    }
}
