using System.Text.Json.Serialization;

namespace CoinMarketCalApi.Entities
{
    public abstract class BaseResponse<T>
    {
        [JsonPropertyName("body")]
        public T[] Body { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }
}
