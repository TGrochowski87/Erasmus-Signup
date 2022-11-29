using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace UserApi.Utilities
{
    public class OAuthTool
    {
        protected Random random = new Random();

        protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        /// <summary>
        /// Helper function to compute a hash value
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
        /// <param name="data">The data to hash</param>
        /// <returns>a Base64 string of the hash value</returns>
        private string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public string UrlEncode(string value)
        {
            StringBuilder result = new StringBuilder();
            Encoding enc = new UTF8Encoding(false, true);
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    foreach (var byt in enc.GetBytes(symbol.ToString()))
                        result.Append('%' + String.Format("{0:X2}", byt));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return random.Next(123400, 9999999).ToString();
        }

        /// <summary>
        /// Generate the signature
        /// </summary>
        /// <returns></returns>
        public string GenerateSignature(string httpMethod, string methodUrl, string methodParams, string tokenSecret)
        {
            string signatureBase = httpMethod.ToUpper() + "&" + UrlEncode(methodUrl) + "&" + UrlEncode(methodParams);
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(Secrets.OAuthApiConsumerSecret), string.IsNullOrWhiteSpace(tokenSecret) ? "" : UrlEncode(tokenSecret)));
            return ComputeHash(hmacsha1, signatureBase);

        }
    }
}
