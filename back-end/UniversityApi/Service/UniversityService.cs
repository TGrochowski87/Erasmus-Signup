using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using UniversityApi.DbModels;
using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApi.Service
{
    public class UniversityService : IUniversityService
    {
        List<DestSpeciality> DestinationsCache = new List<DestSpeciality>();


        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }



        public async Task<DestinationResult> GetListAsync(DestinationCriteria criteria)
        {
            string cacheKey = "Destinations";
            bool cachEnable = true;

            var page = criteria.Page ?? 1;
            var pageSize = criteria.PageSize ?? 10;
            var totalRows = 0;
            IEnumerable<DestSpeciality> list = new List<DestSpeciality>();

            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;

            if (cachEnable && cache.Contains(cacheKey))
                list = (IEnumerable<DestSpeciality>)cache.Get(cacheKey);
            else
            {
                list = await _universityRepository.GetListAsync();

                if (cachEnable)
                {
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(2.0);
                    cache.Add(cacheKey, list, cacheItemPolicy);
                }
            }


            totalRows = list.Count();
            var filterList = list.Skip((page - 1) * pageSize).Take(pageSize);

            return new DestinationResult(filterList.Select(x => new DestinationVM(x)), totalRows);
        }

        public async Task<UniversityGetVM> GetAsync(short destId)
        {
            var university = await _universityRepository.GetAsync(destId);

            return new UniversityGetVM(university, destId);
        }
    }
}
