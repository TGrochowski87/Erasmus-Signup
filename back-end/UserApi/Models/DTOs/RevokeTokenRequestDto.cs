namespace UserApi.Models.DTOs
{
  public class RevokeTokenRequestDto
  {
    public string OAuthToken { get; set; }
    
    public string OAuthTokenSecret { get; set; }

    public void Deconstruct(out string oAuthToken, out string oAuthTokenSecret)
    {
      oAuthToken = OAuthToken;
      oAuthTokenSecret = OAuthTokenSecret;
    }
  }
}