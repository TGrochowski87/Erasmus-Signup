namespace UserApi.Models
{
    public class UserJWT
    {
        public string UserId { get; set; }
        public string OAuthAccessToken { get; set; }
        public string OAuthAccessTokenSecret { get; set; }
        public bool IsSuccess { get; set; }

        public UserJWT(bool isSucces)
        {
            UserId = "";
            OAuthAccessToken = "";
            OAuthAccessTokenSecret = "";
            IsSuccess = isSucces;
        }

        public UserJWT(string userId, string oAuthAccessToken, string oAuthAccessTokenSecret, bool isSuccess)
        {
            UserId = userId;
            OAuthAccessToken = oAuthAccessToken;
            OAuthAccessTokenSecret = oAuthAccessTokenSecret;
            IsSuccess = isSuccess;
        }
    }
}
