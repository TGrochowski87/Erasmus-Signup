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



        public async Task<DestinationResult> GetListAsync(DestinationCriteria criteria)
        {
            var page = criteria.Page ?? 1;
            var pageSize = criteria.PageSize ?? 10;
            var totalRows = 0;

            var list = await _universityRepository.GetListAsync(criteria);

            totalRows = list.Count();
            var filterList = list.Skip((page - 1) * pageSize).Take(pageSize);

            return new DestinationResult(filterList.Select(x => new DestinationVM(x)), new List<DestinationVM>(), new List<DestinationVM>(), totalRows);
        }

        public async Task<UniversityGetVM> GetAsync(short destId)
        {
            var university = await _universityRepository.GetAsync(destId);

            return new UniversityGetVM(university, destId);
        }
    }
}
