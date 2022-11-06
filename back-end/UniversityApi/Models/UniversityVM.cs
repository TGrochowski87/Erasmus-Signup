using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    public class UniversityVM
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

        public UniversityVM()
        {

        }

        public UniversityVM(University university)
        {
            Id = university.Id;
            SubjectAreaId = university.SubjectAreaId;
            SubjectAreaName = university.SubjectAreaName;
            UniversityName = university.UniversityName;
            Country = university.Country;
            FlagUrl = university.FlagUrl;
            Website = university.Website;
            AvailablePlaces = university.AvailablePlaces;
            Rating = university.Rating;
            IsObserved = university.IsObserved;
        }
    }
}
