using System.Collections.Generic;

namespace CoinMarketCalApi.Extensions
{
    internal static class EnumerableExtensions
    {
        public static string ToJoinedList<T>(this IEnumerable<T> list)
        {
            return list == null ? null : string.Join(",", list);
        }
    }
}
