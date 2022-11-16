using System.Web;
using System;
using UserApi.Models;
using UserApi.Utilities;
using Microsoft.Extensions.Hosting;
using System.IO;
using FluentAssertions.Equivalency;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }

        public OAuthUrlModel OAuthUrl(string callbackPath)
        {
            var oauth = new OAuth();
            string oauth_callback               = Secrets.ServiceUrl + callbackPath;
            string oauth_consumer_key           = Secrets.OAuthApiConsumerKey;
            string oauth_nonce                  = oauth.GenerateNonce();
            const string oauth_signature_method = "HMAC-SHA1";
            string oauth_timestamp              = oauth.GenerateTimeStamp();
            const string oauth_version          = "1.0";

            string url =    Secrets.OAuthHostUrl + Secrets.OAuthTokenMethod +
                            "?oauth_callback=" + oauth.UrlEncode(oauth_callback) +
                            "&oauth_consumer_key=" + oauth.UrlEncode(oauth_consumer_key) +
                            "&oauth_nonce=" + oauth.UrlEncode(oauth_nonce) +
                            "&oauth_signature_method=" + oauth.UrlEncode(oauth_signature_method) +
                            "&oauth_timestamp=" + oauth.UrlEncode(oauth_timestamp) +
                            "&oauth_version=" + oauth.UrlEncode(oauth_version);

            string oauth_normalized_url;
            string oauth_normalized_params;
            string oauth_signature = oauth.GenerateSignature(
                new System.Uri(url),
                oauth_consumer_key,
                Secrets.OAuthApiConsumerSecret,
                "",
                "",
                "GET",
                oauth_timestamp,
                oauth_nonce,
                out oauth_normalized_url,
                out oauth_normalized_params
           ) ;

            string tokenUrl = Secrets.OAuthHostUrl + Secrets.OAuthTokenMethod + "?" + oauth_normalized_params + "&oauth_signature=" + HttpUtility.UrlEncode(oauth_signature);
            
            var client = new HttpClient
            {
                Timeout = new TimeSpan(0, 2, 0)    // Standard two minute timeout on web service calls.
            };
            var response = client.GetAsync(tokenUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the JSON formatted service response to obtain the user ID.
                string queryString = response.Content.ReadAsStringAsync().Result;
                var querry = System.Web.HttpUtility.ParseQueryString(queryString);
                string oauth_token = !String.IsNullOrEmpty(querry["oauth_token"]) ? querry["oauth_token"]!.ToString() : "";
                string oauth_token_secret = !String.IsNullOrEmpty(querry["oauth_token_secret"]) ?  querry["oauth_token_secret"]!.ToString() : "";
                
                return new OAuthUrlModel(Secrets.OAuthHostUrl + Secrets.OAuthAuthMethod + "?oauth_token=" + oauth_token);
            }
            return new OAuthUrlModel("");
        }
    }
}