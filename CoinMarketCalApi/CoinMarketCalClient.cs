using System;
using System.Collections.Generic;
using System.Net;
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
        private const string BaseUrl = "https://api.coinmarketcal.com/";

        private static readonly DateTime MinDateTime = new DateTime(2017, 11, 25);

        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }

        public static string AccessToken
        {
            get => AuthorizationToken?.AccessToken;
            set
            {
                if (AuthorizationToken == null)
                {
                    AuthorizationToken = new Auth()
                    {
                        AccessToken = value
                    };
                }
                else
                {
                    AuthorizationToken.AccessToken = value;
                }

            }
        }

        public static string GrantType { get; set; } = "client_credentials";

        protected static Auth AuthorizationToken { get; set; }

        private readonly HttpClient _client;
        private bool _isDisposed;
        private static bool _isAuthorizeInProcess;

        public CoinMarketCalClient() : this(new HttpClientHandler())
        {

        }

        public CoinMarketCalClient(HttpClientHandler handler)
        {
            if (handler == null)
                throw new ArgumentNullException($"{nameof(handler)} can't be null");

          
           
            _client = new HttpClient(handler, true)
            {
                BaseAddress = new Uri(BaseUrl)
            };


            if (string.IsNullOrEmpty(AccessToken))
            {
                if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret))
                {
                    throw new ArgumentNullException($"{nameof(ClientId)} or {nameof(ClientSecret)} can't be empty");
                }
                HandleAuthorization().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieve list of coins
        /// </summary>
        /// <returns>List of available coins</returns>
        public Task<IEnumerable<Coin>> Coins()
        {
            return MakeRequest<IEnumerable<Coin>>($"/v1/coins".ApplyParameters(
                new Dictionary<string, string>()
                {
                    {"access_token", AccessToken}
                }));
        }
        /// <summary>
        /// Retrieve list of categories
        /// </summary>
        /// <returns>List of available categories</returns>
        public Task<IEnumerable<Category>> Categories()
        {
            return MakeRequest<IEnumerable<Category>>($"/v1/categories".ApplyParameters(
                new Dictionary<string, string>()
                {
                    {"access_token", AccessToken}
                }));
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
            var builderUri = $"/v1/events".ApplyParameters(new Dictionary<string, string>()
            {
                {"access_token", AccessToken},
                {nameof(request.Page), request?.Page?.ToString()},
                {nameof(request.Categories), request?.Categories?.ToJoinedList()},
                {nameof(request.Coins), request?.Coins?.ToJoinedList()},
                {nameof(request.Max), request?.Max?.ToString()},
                {nameof(request.DateRangeStart), request?.DateRangeStart?.ToString("dd/MM/yyyy")},
                {nameof(request.DateRangeEnd), request?.DateRangeEnd?.ToString("dd/MM/yyyy")},
                {nameof(request.SortBy), request?.SortBy?.GetFriendlyName()},
                {nameof(request.ShowOnly), request?.ShowOnly?.GetFriendlyName()}
               
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
            var builderUri = $"/v1/events".ApplyParameters(new Dictionary<string, string>()
            {
                {"access_token", AccessToken},
                {nameof(page), page.ToString()},
                {nameof(max), max?.ToString()},
                {nameof(coins), coins?.ToJoinedList()}
            });
            return MakeRequest<IEnumerable<Event>>(builderUri);
        }

        protected async Task<T> MakeRequest<T>(string url, bool isAuthorize = false)
        {
            if (!isAuthorize)
            {
                while (_isAuthorizeInProcess)
                {
                    await Task.Delay(100);
                }

                url = url.ApplyParameters(new Dictionary<string, string>() {{"access_token", AccessToken}});
            }
            var response = await _client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await HandleAuthorization();
                response = await _client.GetAsync(url);
            }
            response.EnsureSuccessStatusCode();
            var obj = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            return obj;
        }


        protected bool CheckAuthorization()
        {
            return !string.IsNullOrEmpty(AccessToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task HandleAuthorization()
        {
            try
            {
                _isAuthorizeInProcess = true;
                AuthorizationToken =
                    await MakeRequest<Auth>(
                        $"/oauth/v2/token?grant_type={GrantType}&client_id={ClientId}&client_secret={ClientSecret}",
                        true);
            }
            finally
            {
                _isAuthorizeInProcess = false;
            }
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
