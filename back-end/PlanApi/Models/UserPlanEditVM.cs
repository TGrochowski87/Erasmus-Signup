namespace PlanApi.Models
{
    public class UserPlanEditVM
    {
        public IEnumerable<HomeSubjectEditVM> Subjects { get; set; }

        public UserPlanEditVM(IEnumerable<HomeSubjectEditVM> subjects)
        {
            Subjects = subjects;
        }
    }
}
