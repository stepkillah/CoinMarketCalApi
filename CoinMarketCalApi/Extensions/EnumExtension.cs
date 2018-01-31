using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCalApi.Extensions
{
    internal static class EnumExtension
    {
        public static string GetFriendlyName(this Sorting sort)
        {
            switch (sort)
            {
                case Sorting.CreatedDesc:
                    return "created_desc";
                case Sorting.HotEvents:
                    return "hot_events";
               default:
                    return null;
            }
        }
    }
}
