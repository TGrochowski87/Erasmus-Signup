using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniversityApi.Utilities
{
    static class Secrets
    {
        private const string SecretsFilePath = @"secrets.json";
        static private string jwtKey = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx";

        static public string JwtKey
        {
            set { jwtKey = value; }
            get
            {
                if (String.IsNullOrWhiteSpace(jwtKey)) LoadSecrets();
                return jwtKey;
            }
        }

        static private string getRequiredSecretString(JObject jObj, string valueName)
        {
            if (jObj[valueName] is null)
            {
                throw new Exception("secrets.json has no \"" + valueName + "\" parameter");
            }
            return jObj[valueName]!.ToString();
        }

        static public void LoadSecrets()
        {
            using (StreamReader file = File.OpenText(SecretsFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject secretJson = (JObject)JToken.ReadFrom(reader);
                jwtKey = getRequiredSecretString(secretJson, "jwt_key");
            }
        }
    }
}
