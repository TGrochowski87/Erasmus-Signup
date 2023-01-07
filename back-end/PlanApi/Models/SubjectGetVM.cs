using PlanApi.DbModels;

namespace PlanApi.Models
{
    public class SubjectGetVM
    {
        public int Id { get; set; }
        public HomeSubjectGetVM MappedSubject { get; set; }
        public string Name { get; set; }
        public short Ects { get; set; }

        public SubjectGetVM(Subject model) { 
            Id = model.Id;
            MappedSubject = new HomeSubjectGetVM(model.MappedSubjectNavigation);
            Name = model.Name;
            Ects = model.Ects;
        }
    }
}
