namespace UserApi.Service
{
    public interface IAuthorisedService
    {
        HttpResponseMessage GetOAuthUrl(string callbackPath);
        HttpResponseMessage GetAccesToken(string oauth_token, string oauth_verifier, string oauth_token_secret);
        HttpResponseMessage Call
        (
            string method,
            List<KeyValuePair<string, string>> urlParams,
            bool useOAuth = false,
            string oauth_token_secret = ""
        );
    }
}
