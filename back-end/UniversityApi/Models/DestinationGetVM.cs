using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    public class DestinationGetVM
    {
        public short Id { get; set; }
        public string? SubjectAreaName { get; set; }
        public string? SubjectAreaId { get; set; }
        public int? InterestedStudents { get; set; }
        public int? Places { get; set; }
        public float? Average { get; set; }
        public decimal Rating { get; set; }
        public bool IsObserved { get; set; }
        public bool IsNote { get; set; }
        public string? SubjectLanguageName { get; set; }

        public DestinationGetVM(DestSpeciality dbModel)
        {
            Id = dbModel.Id;
            SubjectAreaId = dbModel.StudyAreaId;
            SubjectAreaName = dbModel.StudyArea?.Description;
            InterestedStudents = dbModel.InterestedStudents;
            Places = 0; //TODO
            Average = dbModel.MinGradeHistories.FirstOrDefault()?.Grade; 
            Rating = 0;  //TODO
            IsObserved = false;  //TODO
            IsNote = false;  //TODO
            SubjectLanguageName = dbModel.SubjectLanguage?.Name;
        }
    }
}
