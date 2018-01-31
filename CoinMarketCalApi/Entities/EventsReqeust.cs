using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCalApi.Entities
{
    public class EventsReqeust
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
        /// Month of the event
        /// </summary>
        public int? Month { get; set; }
        /// <summary>
        /// Year of the event
        /// </summary>
        public int? Year { get; set; }
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
        /// Show Past Event
        /// </summary>
        public bool? ShowPaswEvents { get; set; }
    }
}
