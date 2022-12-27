using UserApi.DbModels;

namespace UserApi.Models
{
    public class StudentGetVM
    {
        public int? PwrSpeciality { get; set; }
        public float? AverageGrade { get; set; }

        public StudentGetVM(Student student)
        {
            PwrSpeciality = student?.PwrSpeciality;
            AverageGrade = student?.AverageGrade;
        }
    }
}
