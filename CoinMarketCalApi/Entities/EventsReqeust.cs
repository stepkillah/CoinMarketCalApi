using System;
using System.Collections.Generic;

namespace CoinMarketCalApi.Entities
{
    public class EventsRequest
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// Maximum amount of events to display per page
        /// </summary>
        public int? Max { get; set; }
        /// <summary>
        /// Coins Ids
        /// </summary>
        public IEnumerable<string> Coins { get; set; }
        /// <summary>
        /// Categories Ids
        /// </summary>
        public IEnumerable<long> Categories { get; set; }
        /// <summary>
        /// Sorting parameter
        /// </summary>
        public Sorting? SortBy { get; set; }
        /// <summary>
        /// Sorting parameter
        /// </summary>
        public ShowOnly? ShowOnly { get; set; }
        /// <summary>
        /// Events from that date
        /// </summary>
        public DateTime? DateRangeStart { get; set; }
        /// <summary>
        /// Events to that date
        /// </summary>
        public DateTime? DateRangeEnd { get; set; }

        public bool? ShowViews { get; set; }
        public bool? ShowVotes { get; set; }

        /// <summary>
        /// "en" "ko" "ru" "tr" "ja" "es" "pl" "pt" "id"
        /// </summary>
        public string Translations { get; set; }
    }
}
