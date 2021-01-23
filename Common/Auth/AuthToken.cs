using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Common.Auth
{
    public class AuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

    }
}
