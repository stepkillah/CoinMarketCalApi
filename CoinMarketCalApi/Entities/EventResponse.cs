using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoinMarketCalApi.Response
{
    public class EventResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("coin_name")]
        public string Name { get; set; }
        [JsonProperty("coin_symbol")]
        public string Symbol { get; set; }
        [JsonProperty("date_event")]
        public DateTime Date { get; set; }
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("proof")]
        public string Proof { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("is_hot")]
        public bool Hot { get; set; }
        [JsonProperty("vote_count")]
        public long? VoteCount { get; set; }
        [JsonProperty("positive_vote_count")]
        public long? PositiveVoteCount { get; set; }
        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }
        [JsonProperty("categories")]
        public IEnumerable<string> Categories { get; set; }
        [JsonProperty("tip_symbol")]
        public string TipSymbol { get; set; }
        [JsonProperty("tip_adress")]
        public string TipAddress { get; set; }
        [JsonProperty("twitter_account")]
        public string TwitterAccount { get; set; }
        [JsonProperty("event_is_deadline")]
        public bool Deadline { get; set; }

    }
}
