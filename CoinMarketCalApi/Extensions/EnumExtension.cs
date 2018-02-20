using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCalApi.Extensions
{
    internal static class EnumExtension
    {
	    private const string HotEvents = "hot_events";

        public static string GetFriendlyName(this Sorting sort)
        {
            switch (sort)
            {
                case Sorting.CreatedDesc:
                    return "created_desc";
                case Sorting.HotEvents:
                    return HotEvents;
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
			    default:
				    return null;
		    }
	    }
	}
}
