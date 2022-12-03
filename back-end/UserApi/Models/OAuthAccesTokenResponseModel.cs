namespace UserApi.Models
{
    public class OAuthAccesTokenResponseModel
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string UserApiToken { get; set; }

        public OAuthAccesTokenResponseModel(string accessToken, string accessTokenSecret, string userApiToken)
        {
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            UserApiToken = userApiToken;
        }
    }
}
