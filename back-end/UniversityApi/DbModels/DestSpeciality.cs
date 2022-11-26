namespace UniversityApi.DbModels
{
    public partial class DestSpeciality
    {
        public DestSpeciality()
        {
            MinGradeHistories = new HashSet<MinGradeHistory>();
        }

        public short Id { get; set; }
        public string? DestUniversityCode { get; set; }
        public short? ContractDetailsId { get; set; }
        public string? StudyAreaId { get; set; }
        public short? SubjectLanguageId { get; set; }
        public int? InterestedStudents { get; set; }

        public virtual ContractDetail? ContractDetails { get; set; }
        public virtual University? DestUniversityCodeNavigation { get; set; }
        public virtual StudyArea? StudyArea { get; set; }
        public virtual SubjectLanguage? SubjectLanguage { get; set; }
        public virtual ICollection<MinGradeHistory> MinGradeHistories { get; set; }
    }
}
