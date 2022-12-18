namespace UserApi.Service
{
    public interface IAuthorizedService
    {
        HttpResponseMessage GetOAuthUrl(string callbackPath);
        HttpResponseMessage GetAccessToken(string oauth_token, string oauth_verifier, string oauth_token_secret);
        HttpResponseMessage PostRevokeToken(string oauth_token, string oauth_token_secret);
    }
}
