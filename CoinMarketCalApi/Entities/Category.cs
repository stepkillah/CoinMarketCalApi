using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoinMarketCalApi.Entities
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
