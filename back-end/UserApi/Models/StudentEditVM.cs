using UserApi.DbModels;

namespace UserApi.Models
{
    public class StudentEditVM
    {
        public short PreferencedStudyDomainId { get; set; }
        public double AverageGrade { get; set; }

        public UserProfile ToModel(int userId)
        {
            return new UserProfile()
            {
                PreferencedStudyDomainId = PreferencedStudyDomainId,
                AverageGrade = AverageGrade,
                UserId = userId
            };
        }
    }
}
