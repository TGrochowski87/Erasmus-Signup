using UserApi.Utilities;
using UserApi.Service;

namespace UserApi.Service
{
    public class UserService : IUserService
    {

        public HttpResponseMessage GetCurrentUser(string acces_token, string acces_token_secret, IAuthorisedService authorisedService)
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

        public HttpResponseMessage GetCurrentUserId(string acces_token, string acces_token_secret, IAuthorisedService authorisedService)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", acces_token));
            urlParams.Add(new KeyValuePair<string, string>("fields","id" ));
            return authorisedService.Call("services/users/user", urlParams, true, acces_token_secret);
        }
    }
}