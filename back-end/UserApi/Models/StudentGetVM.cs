using Newtonsoft.Json;
using UserApi.DbModels;

namespace UserApi.Models
{
    public class StudentGetVM
    {
        public short? PreferencedStudyDomainId { get; set; } 
        public double? AverageGrade { get; set; }

        [JsonConstructor]
        public StudentGetVM(short? preferencedStudyDomainId, double? averageGrade)
        {
            PreferencedStudyDomainId = preferencedStudyDomainId;
            AverageGrade = averageGrade;
        }

        public StudentGetVM(UserProfile profile)
        {
            PreferencedStudyDomainId = profile?.PreferencedStudyDomainId;
            AverageGrade = profile?.AverageGrade;
        }
    }
}
