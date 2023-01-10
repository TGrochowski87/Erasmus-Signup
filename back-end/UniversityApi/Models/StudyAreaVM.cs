using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    public class StudyAreaVM
    {
        public string Id { get; set; } = null!;

        public string? AreaName { get; set; }

        public StudyAreaVM(StudyArea model)
        {
            Id = model.Id;
            AreaName = model.AreaName;
        }
    }
}
