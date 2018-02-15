using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinMarketCalApi.Entities
{
    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("coins")]
		public IEnumerable<Coin> Coins { get; set; }
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
        [JsonProperty("can_occur_before")]
        public bool CanOccurBefore { get; set; }

    }


	public class Coin
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("symbol")]
		public string Symbol { get; set; }
	}
}
