namespace UserApi.Service
{
    public interface IUserService 
    {
        HttpResponseMessage GetCurrentUser(string acces_token, string acces_token_secret, IAuthorisedService authorisedService);
    }
}
