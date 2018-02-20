using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoinMarketCalApi.Entities;
using CoinMarketCalApi.Extensions;
using Newtonsoft.Json;

namespace CoinMarketCalApi
{
    public class CoinMarketCalClient : ICoinMarketCalClient
    {
        private const string BaseUrl = "https://coinmarketcal.com/";

	    private static readonly DateTime MinDateTime = new DateTime(2017,11,25);

	    private readonly HttpClient _client;
        private bool _isDisposed;

        public CoinMarketCalClient():this(new HttpClientHandler())
        {
        }

        public CoinMarketCalClient(HttpClientHandler handler)
        {
            if(handler==null)
                throw new ArgumentNullException($"{nameof(handler)} can't be null");

            _client = new HttpClient(handler,true)
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

	    /// <summary>
	    /// Retrieve list of coins
	    /// </summary>
	    /// <returns>List of available coins</returns>
		public Task<IEnumerable<string>> Coins()
        {
            return MakeRequest<IEnumerable<string>>("/api/coins");
        }
	    /// <summary>
	    /// Retrieve list of categories
	    /// </summary>
	    /// <returns>List of available categories</returns>
		public Task<IEnumerable<string>> Categories()
        {
            return MakeRequest<IEnumerable<string>>("/api/categories");
        }
	    /// <summary>
	    /// Retrieve list of events
	    /// </summary>
	    /// <param name="request">Reqeust entity with filters</param>
	    /// <exception cref="ArgumentException">You can not fetch event before the date of 25/11/2017</exception>  
		public Task<IEnumerable<Event>> Events(EventsRequest request = null)
        {
	        if ((request?.DateRangeStart.HasValue ?? false) && request.DateRangeStart < MinDateTime)
	        {
		        throw new ArgumentException($"You can not fetch event before the date of 25/11/2017");
	        }
            var builderUri = "/api/events".ApplyParameters(new Dictionary<string, string>()
            {
                {nameof(request.Page), request?.Page?.ToString()},
                {nameof(request.Categories), request?.Categories?.ToJoinedList()},
                {nameof(request.Coins), request?.Coins?.ToJoinedList()},
                {nameof(request.Max), request?.Max?.ToString()},
                {nameof(request.DateRangeStart), request?.DateRangeStart?.ToString("dd/MM/yyyy")},
                {nameof(request.DateRangeEnd), request?.DateRangeEnd?.ToString("dd/MM/yyyy")},
                {nameof(request.SortBy), request?.SortBy?.GetFriendlyName()},
	            {nameof(request.ShowOnly), request?.ShowOnly?.GetFriendlyName()},
			});
            return MakeRequest<IEnumerable<Event>>(builderUri);
        }
	    /// <summary>
	    /// Retrieve list of events
	    /// </summary>
	    /// <param name="page">Page number</param>
	    /// <param name="max">Maximum amount of events to display per page</param>
	    /// <param name="coins">Coins</param>
		public Task<IEnumerable<Event>> Events(int page, int? max = null, IEnumerable<string> coins = null)
        {
            var builderUri = "/api/events".ApplyParameters(new Dictionary<string, string>()
            {
                {nameof(page), page.ToString()},
                {nameof(max), max?.ToString()},
                {nameof(coins), coins?.ToJoinedList()}

            });
            return MakeRequest<IEnumerable<Event>>(builderUri);
        }

        private async Task<T> MakeRequest<T>(string url)
        {
            var result = await _client.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<T>(result);
            return obj;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        public void Dispose() => this.Dispose(true);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        internal virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _client?.Dispose();
                }
                _isDisposed = true;
            }
        }
    }
}
