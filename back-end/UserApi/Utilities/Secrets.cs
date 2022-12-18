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

        #region Functions

        static private string getRequiredSecretString(JObject jObj, string valueName)
        {
            if(jObj[valueName] is null)
            {
                throw new Exception("secrets.json has no \""+ valueName + "\" parameter");
            }
            return jObj[valueName]!.ToString();
        }

        static public void LoadSecrets()
        {
            using (StreamReader file = File.OpenText(SecretsFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject secretJson = (JObject)JToken.ReadFrom(reader);
                serviceUrl              = getRequiredSecretString(secretJson, "service_url");
                oauthHostUrl            = getRequiredSecretString(secretJson, "oauth_host");
                oauthApiConsumerKey     = getRequiredSecretString(secretJson, "oauth_consumer_key");
                oauthApiConsumerSecret  = getRequiredSecretString(secretJson, "oauth_consumer_secret");
                oauthTokenMethod        = getRequiredSecretString(secretJson, "oauth_token_method");
                oauthAuthMethod         = getRequiredSecretString(secretJson, "oauth_auth_method");
                jwtKey                  = getRequiredSecretString(secretJson, "jwt_key");
            }   
        }
        #endregion
    }
}

