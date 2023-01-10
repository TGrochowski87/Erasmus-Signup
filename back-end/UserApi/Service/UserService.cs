using UserApi.Utilities;
using UserApi.Service;
using UserApi.Repository;
using UserApi.Models;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task StudentEdit(StudentEditVM viewModel, int userId)
        {
            await userRepository.ProfileEditAsync(viewModel.ToModel(userId));
        }

        public async Task<StudentGetVM> GetStudent(int userId)
        {
            var student = await userRepository.GetProfileAsync(userId);

            return new StudentGetVM(student);
        }

        public HttpResponseMessage GetCurrentUser(string access_token, string access_token_secret)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", access_token));
            urlParams.Add(new KeyValuePair<string, string>(
                "fields",
                "id" +
                "|first_name" +
                "|middle_names" +
                "|last_name" +
                "|sex" +
                "|titles" +
                "|student_status" +
                "|staff_status" +
                "|email" +
                "|photo_urls[" +
                    "50x50" +
                    "|400x500" +
                "]" +
                "|student_number"
            ));
            return OAuthTool.CallAuthorizedService("services/users/user", urlParams, true, access_token_secret);
        }

        public HttpResponseMessage GetCurrentUserId(string access_token, string access_token_secret)
        {   
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", access_token));
            urlParams.Add(new KeyValuePair<string, string>("fields","id" ));
            return OAuthTool.CallAuthorizedService("services/users/user", urlParams, true, access_token_secret);
        }

        /*
        public HttpResponseMessage GetStudentProgrammes(string userId, string access_token, string access_token_secret)
        {
            List<KeyValuePair<string, string>> grurlParams = new List<KeyValuePair<string, string>>();
            grurlParams.Add(new KeyValuePair<string, string>("oauth_token", access_token));
            //grurlParams.Add(new KeyValuePair<string, string>("fields",
            //    "course_unit_id" +
            //    "|group_number" +
            //    "|class_type" +
            //    "|course_id" +
            //    "|course_name" +
            //    "|course_fac_id" +
            //    "|lecturers" +
            //    "|participants"
            //    ));
            HttpResponseMessage grUser = OAuthTool.CallAuthorizedService("services/courses/user", grurlParams, true, access_token_secret);
            string result = grUser.Content.ReadAsStringAsync().Result;

            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", access_token));
            urlParams.Add(new KeyValuePair<string, string>("fields", "id|programme|status|stages"));
            return OAuthTool.CallAuthorizedService("services/progs/student", urlParams, true, access_token_secret);
        }
        public HttpResponseMessage GetCoordinatorCourses(string userId, string access_token, string access_token_secret)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", access_token));
            return OAuthTool.CallAuthorizedService("services/courses/coordinator", urlParams, true, access_token_secret);
        }
        */
    }
}
