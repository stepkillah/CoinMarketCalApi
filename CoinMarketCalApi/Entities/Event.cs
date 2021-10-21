using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarketCalApi.Entities
{
    public class Event
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")]
        public EventTitle Title { get; set; }
        [JsonPropertyName("coins")]
        public IEnumerable<Coin> Coins { get; set; }
        [JsonPropertyName("date_event")]
        public DateTime Date { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("proof")]
        public string Proof { get; set; }
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("is_hot")]
        public bool Hot { get; set; }
        [JsonPropertyName("vote_count")]
        public long? VoteCount { get; set; }
        [JsonPropertyName("positive_vote_count")]
        public long? PositiveVoteCount { get; set; }
        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
        [JsonPropertyName("categories")]
        public IEnumerable<Category> Categories { get; set; }
        [JsonPropertyName("tip_symbol")]
        public string TipSymbol { get; set; }
        [JsonPropertyName("tip_adress")]
        public string TipAddress { get; set; }
        [JsonPropertyName("twitter_account")]
        public string TwitterAccount { get; set; }
        [JsonPropertyName("can_occur_before")]
        public bool CanOccurBefore { get; set; }

    }

}
