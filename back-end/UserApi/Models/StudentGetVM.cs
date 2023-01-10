using UserApi.DbModels;

namespace UserApi.Models
{
    public class StudentGetVM
    {
        public short? PreferencedStudyDomainId { get; set; } 
        public double? AverageGrade { get; set; }

        public StudentGetVM(UserProfile profile)
        {
            PreferencedStudyDomainId = profile?.PreferencedStudyDomainId;
            AverageGrade = profile?.AverageGrade;
        }
    }
}
