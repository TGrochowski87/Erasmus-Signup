namespace UniversityApi.DbModels
{
    public partial class ExcelFormat
    {
        public string? ErasmusCode { get; set; }
        public string? Country { get; set; }
        public string? UniversityName { get; set; }
        public bool? AcceptingUndergraduate { get; set; }
        public string? UndergraduateYearRestriction { get; set; }
        public bool? AcceptingPostgraduate { get; set; }
        public string? PostgraduateYearRestriction { get; set; }
        public bool? AcceptingDoctoral { get; set; }
        public string? DoctoralYearRestriction { get; set; }
        public string? StudyDomain { get; set; }
        public string? AreaDescription { get; set; }
        public string? SubLanguage { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
