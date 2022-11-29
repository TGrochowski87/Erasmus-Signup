using UserApi.Utilities;
using UserApi.Service;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        public HttpResponseMessage GetCurrentUser(string acces_token, string acces_token_secret, IAuthorisedService authorisedService)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", acces_token));
            urlParams.Add(new KeyValuePair<string, string>("fields", "id|first_name|middle_names|last_name|sex|titles|student_status|staff_status|email|photo_urls[50x50|400x500]|student_number"));
            return authorisedService.Call("services/users/user", urlParams, true, acces_token_secret);
        }
    }
}