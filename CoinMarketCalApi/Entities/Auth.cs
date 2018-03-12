using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoinMarketCalApi.Entities
{
    public class Auth
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonIgnore]
        public DateTime ExpireTime => !string.IsNullOrEmpty(ExpiresIn) ? DateTime.Now.AddSeconds(double.Parse(ExpiresIn)) : DateTime.Now;
    }
}
