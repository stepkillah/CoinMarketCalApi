using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoinMarketCalApi.Entities
{
    public class Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
