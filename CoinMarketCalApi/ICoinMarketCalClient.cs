using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarketCalApi.Entities;

namespace CoinMarketCalApi
{
    public interface ICoinMarketCalClient : IDisposable
    {
        /// <summary>
        /// Retrieve list of coins
        /// </summary>
        /// <returns>List of available coins</returns>
        Task<CoinsResponse> Coins();
        /// <summary>
        /// Retrieve list of categories
        /// </summary>
        /// <returns>List of available categories</returns>
        Task<CategoriesResponse> Categories();
        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="request">Reqeust entity with filters</param>
        /// <exception cref="ArgumentException">You can not fetch event before the date of 25/11/2017</exception>  
        Task<EventsResponse> Events(EventsRequest request = null);
        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="max">Maximum amount of events to display per page</param>
        /// <param name="coins">Coins</param>
        Task<EventsResponse> Events(int page, int? max = null, IEnumerable<string> coins = null);
    }
}
