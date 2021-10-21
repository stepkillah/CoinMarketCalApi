using System.Text.Json.Serialization;

namespace CoinMarketCalApi.Entities
{
    public class Coin
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("fullname")]
        public string FullName { get; set; }
    }
}
