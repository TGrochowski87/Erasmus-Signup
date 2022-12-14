namespace UserApi.Service
{
    public interface IUserService 
    {
        HttpResponseMessage GetCurrentUser(string access_token, string access_token_secret);
        HttpResponseMessage GetCurrentUserId(string access_token, string access_token_secret);
    }
}
