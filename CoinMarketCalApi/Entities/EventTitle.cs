using System.Text.Json.Serialization;

namespace CoinMarketCalApi.Entities
{
    public class EventTitle
    {
        [JsonPropertyName("en")]
        public string En { get; set; }
        [JsonPropertyName("ko")]
        public string Ko { get; set; }
        [JsonPropertyName("ru")]
        public string Ru { get; set; }
        [JsonPropertyName("tr")]
        public string Tr { get; set; }
        [JsonPropertyName("ja")]
        public string Ja { get; set; }
        [JsonPropertyName("es")]
        public string Es { get; set; }
        [JsonPropertyName("pt")]
        public string Pt { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
