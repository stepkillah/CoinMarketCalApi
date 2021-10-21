using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using CoinMarketCalApi.Entities;
using CoinMarketCalApi.Extensions;

namespace CoinMarketCalApi
{
    public class CoinMarketCalClient : ICoinMarketCalClient
    {
        #region private fields
        private const string BaseUrl = "https://developers.coinmarketcal.com/";

        private static readonly DateTime MinDateTime = new DateTime(2017, 11, 25);

        /// <summary>
        /// API key
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// Enabled deflate compression. Disabled by default
        /// </summary>
        private readonly bool _enableDeflate;

        private HttpClient _client;

        private bool _isDisposed;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates client
        /// </summary>
        /// <param name="apiKey">API key</param>
        public CoinMarketCalClient(string apiKey) : this(apiKey, new HttpClientHandler(), false)
        {
        }

        /// <summary>
        /// Creates client
        /// </summary>
        /// <param name="apiKey">API key</param>
        /// <param name="enableDeflate">Indicating if deflate compression should be applied to HttpClient</param>
        public CoinMarketCalClient(string apiKey, bool enableDeflate) : this(apiKey, new HttpClientHandler(), enableDeflate)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">API key</param>
        /// <param name="handler">HttpClientHandler for HttpClient</param>
        /// <param name="enableDeflate">Indicating if deflate compression should be applied to HttpClient</param>
        public CoinMarketCalClient(string apiKey, HttpClientHandler handler, bool enableDeflate)
        {
            _apiKey = apiKey;
            _enableDeflate = enableDeflate;

            if (handler == null)
                throw new ArgumentNullException($"{nameof(handler)} can't be null");

            handler.AutomaticDecompression = DecompressionMethods.GZip;

            _client = new HttpClient(handler, true)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            _client.DefaultRequestHeaders.Add("x-api-key", _apiKey);
            if (_enableDeflate)
                _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        #endregion

        #region public methods
        /// <summary>
        /// Retrieve list of coins
        /// </summary>
        /// <returns>List of available coins</returns>
        public Task<CoinsResponse> Coins()
        {
            return MakeRequest<CoinsResponse>($"/v1/coins");
        }
        /// <summary>
        /// Retrieve list of categories
        /// </summary>
        /// <returns>List of available categories</returns>
        public Task<CategoriesResponse> Categories()
        {
            return MakeRequest<CategoriesResponse>($"/v1/categories");
        }
        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="request">Reqeust entity with filters</param>
        /// <exception cref="ArgumentException">You can not fetch event before the date of 25/11/2017</exception>  
        public Task<EventsResponse> Events(EventsRequest request = null)
        {
            if ((request?.DateRangeStart.HasValue ?? false) && request.DateRangeStart < MinDateTime)
            {
                throw new ArgumentException($"You can not fetch event before the date of 25/11/2017");
            }

            var queryParams = new Dictionary<string, string>()
            {
                { nameof(request.Page), request?.Page?.ToString() },
                { nameof(request.Categories), request?.Categories?.ToJoinedList() },
                { nameof(request.Coins), request?.Coins?.ToJoinedList() },
                { nameof(request.Max), request?.Max?.ToString() },
                { nameof(request.DateRangeStart), request?.DateRangeStart?.ToString("yyyy-MM-dd") },
                { nameof(request.DateRangeEnd), request?.DateRangeEnd?.ToString("yyyy-MM-dd") },
                { nameof(request.SortBy), request?.SortBy?.GetFriendlyName() },
                { nameof(request.ShowOnly), request?.ShowOnly?.GetFriendlyName() },
                { nameof(request.Translations), request?.Translations },
            };
            if (request?.ShowViews != null)
                queryParams.Add(nameof(request.ShowViews), request.ShowViews.Value.ToString());

            if (request?.ShowVotes != null)
                queryParams.Add(nameof(request.ShowVotes), request.ShowVotes.Value.ToString());

            var builderUri = $"/v1/events".ApplyParameters(queryParams);

            return MakeRequest<EventsResponse>(builderUri);
        }

        /// <summary>
        /// Retrieve list of events
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="max">Maximum amount of events to display per page</param>
        /// <param name="coins">Coins</param>
        public Task<EventsResponse> Events(int page, int? max = null, IEnumerable<string> coins = null)
        {
            var builderUri = $"/v1/events".ApplyParameters(new Dictionary<string, string>()
            {
                {nameof(page), page.ToString()},
                {nameof(max), max?.ToString()},
                {nameof(coins), coins?.ToJoinedList()}
            });
            return MakeRequest<EventsResponse>(builderUri);
        }
        #endregion

        #region Methods
        protected virtual async Task<T> MakeRequest<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (!_enableDeflate)
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            // https://github.com/dotnet/runtime/issues/38022#issuecomment-645714216
            var buffer = await response.Content.ReadAsByteArrayAsync();
            using (var memStream = new MemoryStream(buffer, 2, buffer.Length - 6))
            {
                using (var deflateStream = new DeflateStream(memStream, CompressionMode.Decompress))
                {
                    var obj = await JsonSerializer.DeserializeAsync<T>(deflateStream);
                    return obj;
                }
            }
        } 
        #endregion

        #region Disposing
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
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                _client?.Dispose();
                _client = null;
            }
            _isDisposed = true;
        } 
        #endregion
    }
}
