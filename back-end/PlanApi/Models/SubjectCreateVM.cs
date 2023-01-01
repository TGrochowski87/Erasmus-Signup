using PlanApi.DbModels;

namespace PlanApi.Models
{
    public class SubjectCreateVM
    {
        public int MappedSubject { get; set; }
        public string Name { get; set; }
        public short Ects { get; set; }

        public Subject ToModel(int planId)
        {
            return new Subject
            {
                PlanId = planId,
                MappedSubject = MappedSubject,
                Name = Name,
                Ects = Ects
            };
        }
    }
}
