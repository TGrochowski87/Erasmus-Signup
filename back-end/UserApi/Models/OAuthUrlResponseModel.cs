namespace UserApi.Models
{
    public class OAuthUrlResponseModel
    {
        public string OAuthUrl { get; set; }
        public string OAuthTokenSecret { get; set; }

        public OAuthUrlResponseModel(string oAuthUrl, string oAuthTokenSecret)
        {
            OAuthUrl = oAuthUrl;
            OAuthTokenSecret = oAuthTokenSecret;
        }
    }
}
