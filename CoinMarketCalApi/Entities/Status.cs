using System.Text.Json.Serialization;
using CoinMarketCalApi.Converters;

namespace CoinMarketCalApi.Entities
{
    public class Status
    {
        [JsonPropertyName("error_code")]

        public long ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        [JsonConverter(typeof(SafeNumberToStringConverter))]
        public string ErrorMessage { get; set; }
    }
}
