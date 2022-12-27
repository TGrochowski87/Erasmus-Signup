using UserApi.DbModels;

namespace UserApi.Repository
{
    public interface IUserRepository
    {
        Task StudentEditAsync(Student student);
        Task<Student> GetStudentAsync(int userId);
    }
}
