using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserApi.Utilities
{
    static class Secrets
    {
        const string SecretsFilePath = @"secrets.json";

        static private string serviceUrl = "";
        static public string ServiceUrl
        {
            set { serviceUrl = value; }
            get
            {
                if (serviceUrl == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        serviceUrl = secretJson["service_url"] is not null ? secretJson["service_url"]!.ToString() : "";

                    }
                }
                return serviceUrl;
            }
        }

        static private string oauthHostUrl = "";
        static public string OAuthHostUrl
        {
            set { oauthHostUrl = value; }
            get
            {
                if (oauthHostUrl == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        oauthHostUrl = secretJson["oauth_host"] is not null ? secretJson["oauth_host"]!.ToString() : "";

                    }
                }
                return oauthHostUrl;
            }
        }

        static private string oauthApiConsumerKey = "";
        static public string OAuthApiConsumerKey {
            set { oauthApiConsumerKey = value; }
            get 
            {
                if(oauthApiConsumerKey == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        oauthApiConsumerKey = secretJson["oauth_consumer_key"] is not null ? secretJson["oauth_consumer_key"]!.ToString() : "";
                    }
                }
                return oauthApiConsumerKey;
            }
        }

        static private string oauthApiConsumerSecret = "";
        static public string OAuthApiConsumerSecret
        {
            set { oauthApiConsumerSecret = value; }
            get
            {
                if (oauthApiConsumerSecret == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        oauthApiConsumerSecret = secretJson["oauth_consumer_secret"] is not null ? secretJson["oauth_consumer_secret"]!.ToString() : "";
                    }
                }
                return oauthApiConsumerSecret;
            }
        }

        static private string oauthTokenMethod = "";
        static public string OAuthTokenMethod
        {
            set { oauthTokenMethod = value; }
            get
            {
                if (oauthTokenMethod == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        oauthTokenMethod = secretJson["oauth_token_method"] is not null ? secretJson["oauth_token_method"]!.ToString() : "";
                    }
                }
                return oauthTokenMethod;
            }
        }

        static private string oauthAuthMethod = "";
        static public string OAuthAuthMethod
        {
            set { oauthAuthMethod = value; }
            get
            {
                if (oauthAuthMethod == "")
                {
                    using (StreamReader file = File.OpenText(SecretsFilePath))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject secretJson = (JObject)JToken.ReadFrom(reader);
                        oauthAuthMethod = secretJson["oauth_auth_method"] is not null ? secretJson["oauth_auth_method"]!.ToString() : "";
                    }
                }
                return oauthAuthMethod;
            }
        }
    }
}

