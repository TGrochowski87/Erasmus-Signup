using UniversityApi.DbModels;
using UniversityApi.Helpers;

namespace UniversityApi.Models
{
    public class DestinationVM
    {
        public short DestinationSpecialityId  { get; set; }
        public string? UniversityName { get; set; }
        public string? ErasmusCode { get; set; }
        public string? Country { get; set; }
        public string? SubjectAreaName { get; set; }
        public string? SubjectAreaId { get; set; }
        public string? FlagUrl { get; set; }
        public int? Vacancies { get; set; }
        public float? Average { get; set; }
        public int Opinions { get; set; }
        public decimal Rating { get; set; }
        public bool IsObserved { get; set; }
        public string? Link { get; set; }

        public DestinationVM(DestSpeciality dbModel)
        {
            DestinationSpecialityId = dbModel.Id;
            UniversityName = dbModel.DestUniversityCodeNavigation?.Name;
            ErasmusCode = dbModel.DestUniversityCodeNavigation?.ErasmusCode;
            Country = dbModel.DestUniversityCodeNavigation?.Country;
            SubjectAreaName = dbModel.StudyArea?.AreaName;
            SubjectAreaId = dbModel.StudyArea?.AreaId;
            Vacancies = dbModel.ContractDetails?.VacancyMaxPositions;
            Average = dbModel.MinGradeHistories.FirstOrDefault()?.Grade;
            Opinions = 0; //TODO 
            Rating = 0; //TODO 
            IsObserved = false; //TODO 
            Link = dbModel.DestUniversityCodeNavigation?.Link;
            if (dbModel.DestUniversityCodeNavigation?.Country != null)
                FlagUrl = CountryDictionary.Flags.GetValueOrDefault(dbModel.DestUniversityCodeNavigation.Country);
        }
    }
}
