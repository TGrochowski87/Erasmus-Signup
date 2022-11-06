namespace UniversityApi.DbModels
{
    public class University
    {
        public int Id { get; set; }
        public string? SubjectAreaName { get; set; }
        public int SubjectAreaId { get; set; }
        public string? UniversityName { get; set; }
        public string? Country { get; set; }
        public string? FlagUrl { get; set; }
        public string? Website { get; set; }
        public int AvailablePlaces { get; set; }
        public decimal Rating { get; set; }
        public bool IsObserved { get; set; }
    }
}
