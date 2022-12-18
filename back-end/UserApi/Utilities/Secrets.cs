using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserApi.Utilities
{
    static class Secrets
    {
        #region variables
        private const string SecretsFilePath = @"secrets.json";
        static private string serviceUrl = "";
        static private string oauthHostUrl = "";
        static private string oauthApiConsumerKey = "";
        static private string oauthApiConsumerSecret = "";
        static private string oauthTokenMethod = "";
        static private string oauthAuthMethod = "";
        static private string jwtKey = "";
        #endregion

        #region Setters and getters
        static public string ServiceUrl
        {
            set { serviceUrl = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(serviceUrl)) LoadSecrets();
                return serviceUrl;
            }
        }

        static public string OAuthHostUrl
        {
            set { oauthHostUrl = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(oauthHostUrl)) LoadSecrets();
                return oauthHostUrl;
            }
        }

        static public string OAuthApiConsumerKey {
            set { oauthApiConsumerKey = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(oauthApiConsumerKey)) LoadSecrets();
                return oauthApiConsumerKey;
            }
        }

        static public string OAuthApiConsumerSecret
        {
            set { oauthApiConsumerSecret = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(oauthApiConsumerSecret)) LoadSecrets();
                return oauthApiConsumerSecret;
            }
        }

        static public string OAuthTokenMethod
        {
            set { oauthTokenMethod = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(oauthTokenMethod)) LoadSecrets();
                return oauthTokenMethod;
            }
        }

        static public string OAuthAuthMethod
        {
            set { oauthAuthMethod = value; }
            get
            {
                if(String.IsNullOrWhiteSpace(oauthAuthMethod)) LoadSecrets();
                return oauthAuthMethod;
            }
        }

        static public string JwtKey
        {
            set { jwtKey = value; }
            get
            {
                if (String.IsNullOrWhiteSpace(jwtKey)) LoadSecrets();
                return jwtKey;
            }
        }

        #endregion

        #region Functons

        static public void LoadSecrets()
        {
            using (StreamReader file = File.OpenText(SecretsFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject secretJson = (JObject)JToken.ReadFrom(reader);
                serviceUrl              = secretJson["service_url"] is not null ? secretJson["service_url"]!.ToString() : "";
                oauthHostUrl            = secretJson["oauth_host"] is not null ? secretJson["oauth_host"]!.ToString() : "";
                oauthApiConsumerKey     = secretJson["oauth_consumer_key"] is not null ? secretJson["oauth_consumer_key"]!.ToString() : "";
                oauthApiConsumerSecret  = secretJson["oauth_consumer_secret"] is not null ? secretJson["oauth_consumer_secret"]!.ToString() : "";
                oauthTokenMethod        = secretJson["oauth_token_method"] is not null ? secretJson["oauth_token_method"]!.ToString() : "";
                oauthAuthMethod         = secretJson["oauth_auth_method"] is not null ? secretJson["oauth_auth_method"]!.ToString() : "";
                jwtKey                  = secretJson["jwt_key"] is not null ? secretJson["jwt_key"]!.ToString() : "";
            }
        }
        #endregion
    }
}

