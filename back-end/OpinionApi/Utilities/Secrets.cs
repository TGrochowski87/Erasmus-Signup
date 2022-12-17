using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpinionApi.Utilities
{
    static class Secrets
    {
        private const string SecretsFilePath = @"secrets.json";
        static private string jwtKey = "";

        static public string JwtKey
        {
            set { jwtKey = value; }
            get
            {
                if (String.IsNullOrWhiteSpace(jwtKey)) LoadSecrets();
                return jwtKey;
            }
        }

        static public void LoadSecrets()
        {
            using (StreamReader file = File.OpenText(SecretsFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject secretJson = (JObject)JToken.ReadFrom(reader);
                jwtKey = secretJson["jwt_key"] is not null ? secretJson["jwt_key"]!.ToString() : "";
            }
        }
    }
}
