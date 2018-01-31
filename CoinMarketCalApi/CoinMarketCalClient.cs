using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoinMarketCalApi.Entities;
using CoinMarketCalApi.Extensions;
using CoinMarketCalApi.Response;
using Newtonsoft.Json;

namespace CoinMarketCalApi
{
    public class CoinMarketCalClient : ICoinMarketCalClient
    {
        private const string BaseUrl = "https://coinmarketcal.com/";
        
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


        public Task<IEnumerable<string>> Coins()
        {
            return MakeRequest<IEnumerable<string>>("/api/coins");
        }

        public Task<IEnumerable<string>> Categories()
        {
            return MakeRequest<IEnumerable<string>>("/api/categories");
        }

        public Task<IEnumerable<EventResponse>> Events(EventsRequest request = null)
        {
            
            var builderUri = "/api/events".ApplyParameters(new Dictionary<string, string>()
            {
                {nameof(request.Page), request?.Page?.ToString()},
                {nameof(request.Categories), request?.Categories?.ToJoinedList()},
                {nameof(request.Coins), request?.Coins?.ToJoinedList()},
                {nameof(request.Max), request?.Max?.ToString()},
                {nameof(request.Month), request?.Month?.ToString()},
                {nameof(request.ShowPaswEvents), request?.ShowPaswEvents?.ToString()},
                {nameof(request.Year), request?.Year?.ToString()},
                {nameof(request.SortBy), request?.SortBy?.GetFriendlyName()},
            });
            return MakeRequest<IEnumerable<EventResponse>>(builderUri);
        }

        public Task<IEnumerable<EventResponse>> Events(int page, int? max = null, IEnumerable<string> coins = null)
        {
            var builderUri = "/api/events".ApplyParameters(new Dictionary<string, string>()
            {
                {nameof(page), page.ToString()},
                {nameof(max), max?.ToString()},
                {nameof(coins), coins?.ToJoinedList()}

            });
            return MakeRequest<IEnumerable<EventResponse>>(builderUri);
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
