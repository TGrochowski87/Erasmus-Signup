using System.Text.Json.Serialization;
using Newtonsoft.Json;
using UniversityApi.DbModels;
using UniversityApi.Helpers;

namespace UniversityApi.Models
{
    public class DestinationVM
    {
        [JsonPropertyName("destinationSpecialityId")]
        public short DestinationSpecialityId  { get; set; }
        
        [JsonProperty("universityName")]
        public string? UniversityName { get; set; }
        
        [JsonProperty("erasmusCode")]
        public string? ErasmusCode { get; set; }
        
        [JsonProperty("country")]
        public string? Country { get; set; }
        
        [JsonProperty("subjectAreaName")]
        public string? SubjectAreaName { get; set; }
        
        [JsonProperty("subjectAreaId")]
        public string? SubjectAreaId { get; set; }
        
        [JsonProperty("flagUrl")]
        public string? FlagUrl { get; set; }
        
        [JsonProperty("vacancies")]
        public int? Vacancies { get; set; }
        
        [JsonProperty("average")]
        public float? Average { get; set; }
        
        [JsonProperty("opinions")]
        public int Opinions { get; set; }
        
        [JsonProperty("rating")]
        public decimal Rating { get; set; }
        
        [JsonProperty("isObserved")]
        public bool IsObserved { get; set; }
        
        [JsonProperty("link")]
        public string? Link { get; set; }
        
        [JsonProperty("interestedStudents")]
        public int? InterestedStudents { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public DestinationVM(
            short destinationSpecialityId, 
            string? universityName, 
            string? erasmusCode, 
            string? country, 
            string? subjectAreaName, 
            string? subjectAreaId, 
            string? flagUrl, 
            int? vacancies, 
            float? average, 
            int opinions, 
            decimal rating, 
            bool isObserved, 
            string? link, 
            int? interestedStudents)
        {
            DestinationSpecialityId = destinationSpecialityId;
            UniversityName = universityName;
            ErasmusCode = erasmusCode;
            Country = country;
            SubjectAreaName = subjectAreaName;
            SubjectAreaId = subjectAreaId;
            FlagUrl = flagUrl;
            Vacancies = vacancies;
            Average = average;
            Opinions = opinions;
            Rating = rating;
            IsObserved = isObserved;
            Link = link;
            InterestedStudents = interestedStudents;
        }

        public DestinationVM(DestSpeciality dbModel)
        {
            DestinationSpecialityId = dbModel.Id;
            UniversityName = dbModel.DestUniversityCodeNavigation?.Name;
            ErasmusCode = dbModel.DestUniversityCodeNavigation?.ErasmusCode;
            Country = dbModel.DestUniversityCodeNavigation?.Country;
            SubjectAreaName = dbModel.StudyArea?.AreaName;
            SubjectAreaId = dbModel.StudyArea?.Id;
            Vacancies = dbModel.ContractDetails?.VacancyMaxPositions;
            Average = dbModel.MinGradeHistories.FirstOrDefault()?.Grade;
            Opinions = 0; //TODO 
            Rating = 0; //TODO 
            IsObserved = false; //TODO 
            Link = dbModel.DestUniversityCodeNavigation?.Link;
            InterestedStudents = dbModel.InterestedStudents;
            if (dbModel.DestUniversityCodeNavigation?.Country != null)
                FlagUrl = CountryDictionary.Flags.GetValueOrDefault(dbModel.DestUniversityCodeNavigation.Country);
        }
    }
}
