using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    public class DestinationVM
    {
        public short DestinationSpecialityId  { get; set; }
        public string? UniversityName { get; set; }
        public string? ErasmusCode { get; set; }
        public string? Country { get; set; }
        public string? SubjectAreaName { get; set; }
        public short? SubjectAreaId { get; set; }
        public string? FlagUrl { get; set; }
        public int? InterestedStudents { get; set; }
        public float? Average { get; set; }
        public int Messages { get; set; }
        public decimal Rating { get; set; }
        public bool IsObserved { get; set; }
        public string? Link { get; set; }

        public DestinationVM(DestSpeciality dbModel)
        {
            DestinationSpecialityId = dbModel.Id;
            UniversityName = dbModel.DestUniversityCodeNavigation?.Name;
            ErasmusCode = dbModel.DestUniversityCodeNavigation?.ErasmusCode;
            Country = dbModel.DestUniversityCodeNavigation?.Country;
            SubjectAreaName = dbModel.SubjectLanguage?.Name;
            SubjectAreaId = dbModel.SubjectLanguage?.Id;
            FlagUrl = null; //TODO
            InterestedStudents = dbModel.InterestedStudents;
            Average = dbModel.MinGradeHistories.FirstOrDefault()?.Grade;
            Messages = 0; //TODO 
            Rating = 0; //TODO 
            IsObserved = false; //TODO 
            Link = dbModel.DestUniversityCodeNavigation?.Link; 
        }
    }
}
