using UserApi.Utilities;

namespace UserApi.Service
{
    public interface IUserService 
    {
        HttpResponseMessage GetCurrentUser(string access_token, string access_token_secret);
        HttpResponseMessage GetCurrentUserId(string access_token, string access_token_secret);
        //HttpResponseMessage GetStudentProgrammes(string userId, string access_token, string access_token_secret);
        //HttpResponseMessage GetCoordinatorCourses(string userId, string access_token, string access_token_secret);
    }
}
