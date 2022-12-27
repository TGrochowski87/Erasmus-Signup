namespace UniversityApi.Models
{
    public class DestinationCriteria
    {
        public int? PageSize { get; set; }
        public int? Page { get; set; }
        public string? Country { get; set; }
        public string? SubjectAreaId { get; set; }
        public string? OrderByInterestedStudents { get; set; }
    }
}
