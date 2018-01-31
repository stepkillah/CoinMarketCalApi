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
        [Test]
        public async Task Coins_Test()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Coins();
            Assert.NotNull(result);
            Assert.Greater(result.Count(),1);
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
            var result = await client.Events(1,25);
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_SortingTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest(){SortBy = Sorting.CreatedDesc});
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CoinsTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { Coins = new List<string>() { "Bitcoin (BTC)", "Ripple (XRP)"} });
            Assert.NotNull(result);
        }

        [Test]
        public async Task Events_CategoriesTest()
        {
            var client = new CoinMarketCalClient();
            var result = await client.Events(new EventsRequest() { Categories = await client.Categories() });
            Assert.NotNull(result);
        }
    }
}
