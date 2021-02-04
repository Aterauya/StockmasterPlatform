using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Common.Auth
{
    /// <summary>
    /// The request auth token object
    /// </summary>
    public class RequestAuthToken
    {
        [JsonProperty("client_id")]
        private string ClientId;
        [JsonProperty("client_secret")]
        private string ClientSecret;
        [JsonProperty("audience")]
        private string Audience;
        [JsonProperty("grant_type")]
        private string GrantType;

        public RequestAuthToken(string clientId, string clientSecret, string audience, string grantType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Audience = audience;
            GrantType = grantType;
        }
    }
}
