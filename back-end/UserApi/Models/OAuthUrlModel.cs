namespace UserApi.Models
{
    public class OAuthUrlModel
    {
        public string OAuthUrl { get; set; }

        public OAuthUrlModel(string oAuthUrl)
        {
            OAuthUrl = oAuthUrl;
        }
    }
}
