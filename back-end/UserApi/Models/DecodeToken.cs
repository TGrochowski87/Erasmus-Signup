namespace UserApi.Models
{
    public class DecodeToken
    {
        public string? UserId { get; set; }
        public string? OAuthAccessToken { get; set; }
        public bool IsSuccess { get; set; }

        public DecodeToken(bool isSucces)
        {
            IsSuccess = isSucces;
        }

        public DecodeToken(string userId, string oAuthAccessToken, bool isSuccess)
        {
            UserId = userId;
            OAuthAccessToken = oAuthAccessToken;
            IsSuccess = isSuccess;
        }
    }
}
