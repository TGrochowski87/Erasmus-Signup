using UserApi.DbModels;

namespace UserApi.Models
{
    public class StudentEditVM
    {
        public int PwrSpeciality { get; set; }
        public float AverageGrade { get; set; }

        public Student ToModel(int userId)
        {
            return new Student()
            {
                PwrSpeciality = PwrSpeciality,
                AverageGrade = AverageGrade,
                UserId = userId
            };
        }
    }
}
