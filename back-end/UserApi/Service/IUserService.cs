using UserApi.Models;
using UserApi.Utilities;

namespace UserApi.Service
{
    public interface IUserService 
    {
        HttpResponseMessage GetCurrentUser(string access_token, string access_token_secret);
        HttpResponseMessage GetCurrentUserId(string access_token, string access_token_secret);
        Task StudentEdit(StudentEditVM viewModel, int userId);
        Task<StudentGetVM> GetStudent(int userId);
        //HttpResponseMessage GetStudentProgrammes(string userId, string access_token, string access_token_secret);
        //HttpResponseMessage GetCoordinatorCourses(string userId, string access_token, string access_token_secret);
    }
}
