using PlanApi.DbModels;
using System.Numerics;

namespace PlanApi.Models
{
    public class PlanCreateVM
    {
        public string Name { get; set; }
        public IEnumerable<SubjectCreateVM> Subjects { get; set; }

        public Plan ToModel(long studentId)
        {
            return new Plan {
                StudentId = studentId,
                Name = Name
            };
        }

        public Plan ToModel(int planId, long studentId)
        {
            return new Plan
            {
                Id = planId,
                StudentId = studentId,
                Name = Name
            };
        }
    }
}
