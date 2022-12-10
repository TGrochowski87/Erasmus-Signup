using UserApi.Utilities;

namespace UserApi.Service
{
    public class AuthorizedService : IAuthorizedService
    {

        public HttpResponseMessage GetOAuthUrl(string callbackPath)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            if (callbackPath != "oob")
            {
                callbackPath = Secrets.ServiceUrl + callbackPath;
            }
            urlParams.Add(new KeyValuePair<string, string>("oauth_callback", callbackPath));
            urlParams.Add(new KeyValuePair<string, string>(
                "scopes", 
                "studies" +
                "|staff_perspective" +
                //"|offline_access" +
                "|other_emails" +
                "|email"));
            return OAuthTool.CallAuthorizedService(Secrets.OAuthTokenMethod, urlParams, true);
        }

        public HttpResponseMessage GetAccessToken(string oauth_token, string oauth_verifier, string oauth_token_secret)
        {
            List<KeyValuePair<string,string>> urlParams = new List<KeyValuePair<string,string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", oauth_token));
            urlParams.Add(new KeyValuePair<string, string>("oauth_verifier", oauth_verifier));
            return OAuthTool.CallAuthorizedService("services/oauth/access_token", urlParams, true, oauth_token_secret);
        }

        public HttpResponseMessage PostRevokeToken(string oauth_token, string oauth_token_secret)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", oauth_token));
            return OAuthTool.CallAuthorizedService("services/oauth/revoke_token", urlParams, true, oauth_token_secret);
        }

    }
}
