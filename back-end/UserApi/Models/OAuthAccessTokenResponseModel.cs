namespace UserApi.Models
{
    public class OAuthAccessTokenResponseModel
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string UserApiToken { get; set; }

        public OAuthAccessTokenResponseModel(string accessToken, string accessTokenSecret, string userApiToken)
        {
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            UserApiToken = userApiToken;
        }
    }
}
