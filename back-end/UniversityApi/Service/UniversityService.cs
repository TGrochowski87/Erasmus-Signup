using ErasmusRabbitContracts.UniversityContracts;
using MassTransit;
using UniversityApi.DbModels;
using UniversityApi.Helpers;
using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApi.Service
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        IRequestClient<ProfileGet> _client;

        public UniversityService(IUniversityRepository universityRepository,
            IPublishEndpoint publishEndpoint, IRequestClient<ProfileGet> client)
        {
            _universityRepository = universityRepository;
            _publishEndpoint = publishEndpoint;
            _client = client;
        }



        public async Task<DestinationResult> GetListAsync(DestinationCriteria criteria)
        {
            var page = criteria.Page ?? 1;
            var pageSize = criteria.PageSize ?? 10;
            var totalRows = 0;

            var list = await _universityRepository.GetListAsync(criteria);

            totalRows = list.Count();
            var filterList = list.Skip((page - 1) * pageSize).Take(pageSize);

            return new DestinationResult(filterList.Select(x => new DestinationVM(x)), totalRows);
        }

        public async Task<IEnumerable<DestinationVM>> GetRecommendedDestinations(int userId)
        {
            //var profile = await _publishEndpoint.Publish(new ProfileGet(userId));
            var response = await _client.GetResponse<ProfileGetResult>(new ProfileGet(userId));

            var recomendedDestinations = await _universityRepository.GetListRecommendedDestinationsAsync(null, null); //TODO
            var filterRecomendedDestinations = recomendedDestinations.Take(10);

            return filterRecomendedDestinations.Select(x => new DestinationVM(x));
        }

        public async Task<IEnumerable<DestinationVM>> GetRecommendedByStudentsDestinations()
        {
            var recomendedByStudentsDestinations = await _universityRepository.GetListRecommendedDestinationsAsync(null, null); //TODO
            var filterRecomendedByStudentsDestinations = recomendedByStudentsDestinations.Take(5);

            return filterRecomendedByStudentsDestinations.Select(x => new DestinationVM(x));
        }

        public IEnumerable<string> GetCountries()
        {
            return CountryDictionary.Flags.Select(x => x.Key);
        }

        public async Task<IEnumerable<StudyDomainVM>> GetStudyDomainListAsync()
        {
            var list = await _universityRepository.GetStudyDomainListAsync();

            return list.Select(x => new StudyDomainVM(x));
        }

        public async Task<IEnumerable<StudyAreaVM>> GetStudyAreaListAsync()
        {
            var list = await _universityRepository.GetStudyAreaListAsync();

            return list.Select(x => new StudyAreaVM(x));
        }

        public async Task<UniversityGetVM> GetAsync(short destId)
        {
            var university = await _universityRepository.GetAsync(destId);

            return new UniversityGetVM(university, destId);
        }
    }
}
