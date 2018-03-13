using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMarketCalApi;
using CoinMarketCalApi.Entities;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ClientTest
    {
        
       

        public ClientTest()
        {
            // Update when tests need to be run. Should be permanent in next release.
            CoinMarketCalClient.AccessToken = "your access token";
            CoinMarketCalClient.ClientId = "your client id";
            CoinMarketCalClient.ClientSecret = "your client secret";
        }

        [Test]
        public async Task Coins_Test()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Coins();
            Assert.NotNull(result);
            Assert.Greater(result.Count(), 1);
        }

        [Test]
        public async Task Categories_Test()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Categories();
            Assert.NotNull(result);
            Assert.Greater(result.Count(), 1);
        }

        [Test]
        public async Task Events_NoFiltersTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events();
            Assert.NotNull(result);
            Assert.Greater(result.Count(), 1);
        }

        [Test]
        public async Task Events_PageMaxFilterTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(1, 25);
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_SortingTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { SortBy = Sorting.CreatedDesc });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CoinsTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { Coins = new List<string>() { "bitcoin", "ripple" } });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CategoriesTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { Categories = (await client.Categories()).Select(x => x.Id) });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_DateRangeTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { DateRangeStart = DateTime.Now, DateRangeEnd = DateTime.Now.AddDays(7) });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_DateTomorrowTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { DateRangeStart = DateTime.Today.AddDays(1), DateRangeEnd = DateTime.Now.AddDays(1) });
            Assert.NotNull(result);
            Assert.AreEqual(DateTime.Today.AddDays(1).Date, result.First().Date.Date);
        }

        [Test]
        public async Task Events_OnlyHot()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { ShowOnly = ShowOnly.HotEvents });
            Assert.NotNull(result);
            Assert.AreEqual(true, result.All(x => x.Hot));
        }

        [Test]
        public async Task AuthorizeTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Coins();
            Assert.NotNull(result);
        }



    }
}
