namespace PlanApi.Models
{
    public class UserPlanGetVM
    {
        public IEnumerable<HomeSubjectGetVM> Subjects { get; set; }

        public UserPlanGetVM(IEnumerable<HomeSubjectGetVM> subjects)
        {
            Subjects = subjects;
        }
    }
}
