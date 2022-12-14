namespace UserApi.Models.DTOs
{
  public class GetAccessTokenQueryParams
  {
    public string OAuthToken { get; set; }
    
    public string OAuthVerifier { get; set; }
    
    public string OAuthTokenSecret { get; set; }

    public void Deconstruct(out string oAuthToken, out string oAuthVerifier, out string oAuthTokenSecret)
    {
      oAuthToken = OAuthToken;
      oAuthVerifier = OAuthVerifier;
      oAuthTokenSecret = OAuthTokenSecret;
    }
  }
}