using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinMarketCalApi.Entities;
using CoinMarketCalApi.Response;

namespace CoinMarketCalApi
{
    public interface ICoinMarketCalClient : IDisposable
    {
        /// <summary>
        /// Retrieve list of coins
        /// </summary>
        /// <returns>List of available coins</returns>
        Task<IEnumerable<string>> Coins();
        /// <summary>
        /// Retrieve list of categories
        /// </summary>
        /// <returns>List of available categories</returns>
        Task<IEnumerable<string>> Categories();
        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="request">Reqeust entity with filters</param>
        Task<IEnumerable<EventResponse>> Events(EventsReqeust request = null);
        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="max">Maximum amount of events to display per page</param>
        /// <param name="coins">Coins</param>
        Task<IEnumerable<EventResponse>> Events(int page, int? max = null, IEnumerable<string> coins = null);
    }
}
