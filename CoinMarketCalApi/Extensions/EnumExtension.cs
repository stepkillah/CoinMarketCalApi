using CoinMarketCalApi.Entities;

namespace CoinMarketCalApi.Extensions
{
    internal static class EnumExtension
    {
        private const string HotEvents = "hot_events";

        private const string TrendingEvents = "trending_events";

        private const string SignificantEvents = "significant_events";

        public static string GetFriendlyName(this Sorting sort)
        {
            switch (sort)
            {
                case Sorting.CreatedDesc:
                    return "created_desc";
                case Sorting.HotEvents:
                    return HotEvents;
                case Sorting.TrendingEvents:
                    return TrendingEvents;
                case Sorting.SignificantEvents:
                    return SignificantEvents;
                default:
                    return null;
            }
        }

        public static string GetFriendlyName(this ShowOnly show)
        {
            switch (show)
            {
                case ShowOnly.HotEvents:
                    return HotEvents;
                case ShowOnly.TrendingEvents:
                    return TrendingEvents;
                case ShowOnly.SignificantEvents:
                    return SignificantEvents;
                default:
                    return null;
            }
        }
    }
}
