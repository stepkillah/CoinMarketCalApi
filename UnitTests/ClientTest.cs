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
        private const string ApiKey = "";
        private const bool EnabledDeflate = true;
        
        [Test]
        public async Task Coins_Test()
        {
            var result = await GetClient().Coins();
            Assert.NotNull(result?.Body);
            Assert.Greater(result.Body.Count(), 1);
        }

        [Test]
        public async Task Categories_Test()
        {
            var result = await GetClient().Categories();
            Assert.NotNull(result);
            Assert.Greater(result.Body.Count(), 1);
        }

        [Test]
        public async Task Events_NoFiltersTest()
        {
            var result = await GetClient().Events();
            Assert.NotNull(result);
            Assert.Greater(result.Body.Count(), 1);
        }

        [Test]
        public async Task Events_PageMaxFilterTest()
        {
            var result = await GetClient().Events(1, 25);
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_SortingTest()
        {
            var result = await GetClient().Events(new EventsRequest() { SortBy = Sorting.CreatedDesc });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CoinsTest()
        {
            var result = await GetClient().Events(new EventsRequest() { Coins = new List<string>() { "bitcoin-cash" } });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CategoriesTest()
        {
            var client = GetClient();
            var result = await client.Events(new EventsRequest() { Categories = (await client.Categories()).Body.Select(x => x.Id) });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_DateRangeTest()
        {
            var result = await GetClient().Events(new EventsRequest() { DateRangeStart = DateTime.Now, DateRangeEnd = DateTime.Now.AddDays(7) });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_DateTomorrowTest()
        {
            var result = await GetClient().Events(new EventsRequest() { DateRangeStart = DateTime.Today.AddDays(1), DateRangeEnd = DateTime.Now.AddDays(1) });
            Assert.NotNull(result);
            Assert.AreEqual(DateTime.Today.AddDays(1).Date, result.Body.First().Date.Date);
        }

        private CoinMarketCalClient GetClient() => new CoinMarketCalClient(ApiKey, EnabledDeflate);

    }
}
