using System;
using System.Collections.Generic;
using System.Text;

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
        /// Coins
        /// </summary>
        public IEnumerable<string> Coins { get; set; }
        /// <summary>
        /// Categories
        /// </summary>
        public IEnumerable<string> Categories { get; set; }
        /// <summary>
        /// Sorting parameter
        /// </summary>
        public Sorting? SortBy { get; set; }
	    /// <summary>
	    /// Events from that date
	    /// </summary>
	    public DateTime? DateRangeStart { get; set; }
		/// <summary>
		/// Events to that date
		/// </summary>
		public DateTime? DateRangeEnd { get; set; }
	}
}
