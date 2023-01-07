using PlanApi.DbModels;

namespace PlanApi.Models
{
    public class HomeSubjectGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public short Ects { get; set; }

        public HomeSubjectGetVM(HomeSubject model) {
            Id = model.Id;
            Name = model.Name;
            Ects = model.Ects;
        }
    }
}
