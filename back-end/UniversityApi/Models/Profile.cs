namespace UniversityApi.Models
{
    public class Profile
    {
        public short? PreferencedStudyDomainId { get; set; }
        public double? AverageGrade { get; set; }

        public Profile(short? preferencedStudyDomainId, double? averageGrade)
        {
            PreferencedStudyDomainId = preferencedStudyDomainId;
            AverageGrade = averageGrade;
        }
    }
}
