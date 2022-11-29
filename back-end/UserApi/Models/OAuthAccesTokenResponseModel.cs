namespace UserApi.Models
{
    public class OAuthAccesTokenResponseModel
    {
        public string AccesToken { get; set; }
        public string AccesTokenSecret { get; set; }

        public OAuthAccesTokenResponseModel(string accesToken, string accesTokenSecret)
        {
            AccesToken = accesToken;
            AccesTokenSecret = accesTokenSecret;
        }
    }
}
