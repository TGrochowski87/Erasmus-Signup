using PlanApi.DbModels;

namespace PlanApi.Models
{
    public class PlanGetVM
    {
        public int Id { get; set; }
        public long StudentId { get; set; }
        public string Name { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public IEnumerable<SubjectGetVM> Subjects { get; set; }

        public PlanGetVM(Plan model, string studentName, string studentSurname)
        {
            Id = model.Id;
            StudentId = model.StudentId;
            Name = model.Name;
            StudentName = studentName;
            StudentSurname = studentSurname;
            Subjects = model.Subjects.Select(s => new SubjectGetVM(s));
        }
    }
}
