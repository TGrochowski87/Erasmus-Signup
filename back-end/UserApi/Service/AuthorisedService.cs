using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using UserApi.Utilities;

namespace UserApi.Service
{
    public class AuthorisedService : IAuthorisedService
    {
        OAuthTool oAuthTool = new OAuthTool();

        public HttpResponseMessage GetOAuthUrl(string callbackPath)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            if (callbackPath != "oob")
            {
                callbackPath = Secrets.ServiceUrl + callbackPath;
            }
            urlParams.Add(new KeyValuePair<string, string>("oauth_callback", callbackPath));
            urlParams.Add(new KeyValuePair<string, string>("scopes", "studies|staff_perspective|offline_access|other_emails|email"));
            return Call(Secrets.OAuthTokenMethod, urlParams, true);
        }

        public HttpResponseMessage GetAccesToken(string oauth_token, string oauth_verifier, string oauth_token_secret)
        {
            List<KeyValuePair<string,string>> urlParams = new List<KeyValuePair<string,string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", oauth_token));
            urlParams.Add(new KeyValuePair<string, string>("oauth_verifier", oauth_verifier));
            return Call("services/oauth/access_token", urlParams, true, oauth_token_secret);
        }

        public HttpResponseMessage PostRevokeToken(string oauth_token, string oauth_token_secret)
        {
            List<KeyValuePair<string, string>> urlParams = new List<KeyValuePair<string, string>>();
            urlParams.Add(new KeyValuePair<string, string>("oauth_token", oauth_token));
            return Call("services/oauth/revoke_token", urlParams, true, oauth_token_secret);
        }

        public HttpResponseMessage Call(
            string method,
            List<KeyValuePair<string, string>> urlParams,
            bool useOAuth = false,
            string oauth_token_secret = ""
        )
        {
            string url = Secrets.OAuthHostUrl + method;
            string nonce        = useOAuth ? oAuthTool.GenerateNonce()     : String.Empty;
            string timestamp    = useOAuth ? oAuthTool.GenerateTimeStamp() : String.Empty;
            if (useOAuth)
            {
                urlParams.Add(new KeyValuePair<string, string>("oauth_consumer_key", Secrets.OAuthApiConsumerKey));
                urlParams.Add(new KeyValuePair<string, string>("oauth_nonce", nonce));
                urlParams.Add(new KeyValuePair<string, string>("oauth_signature_method", "HMAC-SHA1"));
                urlParams.Add(new KeyValuePair<string, string>("oauth_timestamp", timestamp));
                urlParams.Add(new KeyValuePair<string, string>("oauth_version", "1.0"));
            }
            string paramsString = String.Empty;
            if(urlParams.Any())
            {
                urlParams.Sort((x, y) => (String.Compare(x.Key, y.Key)));
                paramsString += oAuthTool.UrlEncode(urlParams[0].Key) + "=" + oAuthTool.UrlEncode(urlParams[0].Value);
                for(int i = 1; i < urlParams.Count; i++)
                {
                    paramsString += "&" + oAuthTool.UrlEncode(urlParams[i].Key) + "=" + oAuthTool.UrlEncode(urlParams[i].Value);
                }
            }

            if (useOAuth)
            {
                string oauth_signature = oAuthTool.GenerateSignature("GET", url, paramsString, oauth_token_secret);
                paramsString += "&oauth_signature=" + HttpUtility.UrlEncode(oauth_signature);
            }

            var client = new HttpClient
            {
                Timeout = new TimeSpan(0, 2, 0)
            };
            return client.GetAsync(url + "?" + paramsString).Result;
        }
    }
}
