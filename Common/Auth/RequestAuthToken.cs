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
        /// <summary>
        /// The client identifier
        /// </summary>
        [JsonProperty("client_id")]
        private string _clientId;
        /// <summary>
        /// The client secret
        /// </summary>
        [JsonProperty("client_secret")]
        private string _clientSecret;
        /// <summary>
        /// The audience
        /// </summary>
        [JsonProperty("audience")]
        private string _audience;
        /// <summary>
        /// The grant type
        /// </summary>
        [JsonProperty("grant_type")]
        private string _grantType;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestAuthToken"/> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="audience">The audience.</param>
        /// <param name="grantType">Type of the grant.</param>
        public RequestAuthToken(string clientId, string clientSecret, string audience, string grantType)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _audience = audience;
            _grantType = grantType;
        }
    }
}
