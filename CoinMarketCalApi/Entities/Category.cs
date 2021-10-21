using System.Text.Json.Serialization;

namespace CoinMarketCalApi.Entities
{
    public class Category
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
