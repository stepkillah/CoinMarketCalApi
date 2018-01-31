using System.Collections.Generic;

namespace CoinMarketCalApi.Extensions
{
    internal static class EnumerableExtensions
    {
        public static string ToJoinedList(this IEnumerable<string> list)
        {
            return list == null ? null : string.Join(",", list);
        }
    }
}
