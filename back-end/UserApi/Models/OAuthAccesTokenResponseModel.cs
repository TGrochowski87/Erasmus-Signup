namespace UserApi.Models
{
    public class OAuthAccesTokenResponseModel
    {
        public string AccesToken { get; set; }
        public string AccesTokenSecret { get; set; }
        public string UserApiToken { get; set; }

        public OAuthAccesTokenResponseModel(string accesToken, string accesTokenSecret, string userApiToken)
        {
            AccesToken = accesToken;
            AccesTokenSecret = accesTokenSecret;
            UserApiToken = userApiToken;
        }
    }
}
