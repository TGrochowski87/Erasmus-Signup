using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using UniversityApi.DbModels;
using UniversityApi.Helpers;
using UniversityApi.Models;
using UniversityApi.Repository;

namespace UniversityApi.Service
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly HttpClient _httpClientUser;
        private readonly HttpClient _httpClientNote;

        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;

            _httpClientUser = new HttpClient();
            _httpClientUser.BaseAddress = new Uri("https://localhost:7077/");
            _httpClientUser.Timeout = new TimeSpan(0, 2, 0);

            _httpClientNote = new HttpClient();
            _httpClientNote.BaseAddress = new Uri("https://localhost:7169/");
            _httpClientNote.Timeout = new TimeSpan(0, 2, 0);
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

        public async Task<RecommendedDestination> GetRecommendedDestinations(UserJWT userJWT)
        {
            var profile = await GetProfile(int.Parse(userJWT.UserId), userJWT.Token);

            if (profile.AverageGrade == null && profile.PreferencedStudyDomainId == null)
            {
                return new RecommendedDestination() { IsCompletedProfile = false, Destinations = new List<DestinationVM>() };
            }

            var recomendedDestinations = await _universityRepository.GetListRecommendedDestinationsAsync(profile.PreferencedStudyDomainId, (float?)profile.AverageGrade);
            var filterRecomendedDestinations = recomendedDestinations.Take(10);

            return new RecommendedDestination() { IsCompletedProfile = true, Destinations = filterRecomendedDestinations.Select(x => new DestinationVM(x)) };
        }

        public async Task<RecommendedDestination> GetRecommendedByStudentsDestinations(UserJWT userJWT)
        {
            List<DestSpeciality> recommendedDest = new List<DestSpeciality>();

            var profile = await GetProfile(int.Parse(userJWT.UserId), userJWT.Token);

            if (profile.PreferencedStudyDomainId == null)
            {
                return new RecommendedDestination() { IsCompletedProfile = false, Destinations = new List<DestinationVM>() };
            }

            var users = await GetUsersByStudyDomain(profile.PreferencedStudyDomainId.Value, userJWT.Token);

            if (!users.Any())
            {
                return new RecommendedDestination() { IsCompletedProfile = true, Destinations = new List<DestinationVM>() };
            }

            foreach (var user in users)
            {
                if (user != int.Parse(userJWT.UserId))
                {
                    var specIds = await GetSpecListByUser(user, userJWT.Token);

                    if (specIds.Any())
                    {
                        foreach (var specId in specIds)
                        {
                            var spec = await _universityRepository.GetRecommendedByStudentsDestinationsAsync((short)specId);
                            recommendedDest.Add(spec);

                            if (recommendedDest.Count() >= 5)
                                return new RecommendedDestination() { IsCompletedProfile = true, Destinations = recommendedDest.Select(x => new DestinationVM(x)).Take(5) };
                        }
                    }
                }
            }
            
            return new RecommendedDestination() { IsCompletedProfile = true, Destinations = recommendedDest.Select(x => new DestinationVM(x)).Take(5) };
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

        public async Task<DestinationResult> GetListForUserAsync(UserDestinationCriteria criteria)
        {
            var list = await _universityRepository.GetListForUserAsync(criteria);

            return new DestinationResult(list.Select(x => new DestinationVM(x)), list.Count());
        }

        private async Task<Profile> GetProfile(int userId, string token)
        {
            _httpClientUser.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClientUser.GetAsync("user/profiles");
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            JObject jProfile = JObject.Parse(await result.Content.ReadAsStringAsync());
            short? preferencedStudyDomainId = (short?)jProfile["preferencedStudyDomainId"];
            double? averageGrade = (double?)jProfile["averageGrade"];

            return new Profile(preferencedStudyDomainId, averageGrade);
        }

        private async Task<IEnumerable<int>> GetUsersByStudyDomain(short studyDomainId, string token)
        {
            _httpClientUser.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClientUser.GetAsync($"user/study-domain/{studyDomainId}");
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            var list = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<int>>(list); 
        }

        private async Task<IEnumerable<int>> GetSpecListByUser(int userId, string token)
        {
            _httpClientNote.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClientNote.GetAsync($"note/favorite-spec/{userId}");
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            var list = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<int>>(list);
        }
    }
}
