using PlanApi.DbModels;

namespace PlanApi.Models
{
    public class HomeSubjectEditVM
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public short Ects { get; set; }

        public HomeSubject ToModel(long studentId)
        {
            return new HomeSubject
            {
                Id = (int)(Id == null ? 0 : Id),
                StudentId = studentId,
                Name = Name,
                Ects = Ects
            };
        }
    }
}
