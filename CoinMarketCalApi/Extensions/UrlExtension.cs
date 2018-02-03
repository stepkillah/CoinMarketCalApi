using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinMarketCalApi.Extensions
{
    /// <summary>
    /// Extensions for uris.
    /// </summary>
    internal static class UriExtensions
    {
	    static string FirstCharacterToLower(string str)
	    {
		    if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
			    return str;

		    return Char.ToLowerInvariant(str[0]) + str.Substring(1);
	    }
		/// <summary>
		/// Merge a dictionary of values with an existing <see cref="Uri"/>
		/// </summary>
		/// <param name="uri">Original request Uri</param>
		/// <param name="parameters">Collection of key-value pairs</param>
		/// <returns>Updated request Uri</returns>
		public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            if (uri == null)
                throw new ArgumentNullException($"{nameof(uri)} can't be null");

            if (parameters == null || !parameters.Any())
            {
                return uri;
            }

            // to prevent values being persisted across requests
            // use a temporary dictionary which combines new and existing parameters
            IDictionary<string, string> p = new Dictionary<string, string>(parameters);

            var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);

            var uriWithoutQuery =
                hasQueryString == -1 ? uri.ToString() : uri.OriginalString.Substring(0, hasQueryString);

            string queryString;
            if (uri.IsAbsoluteUri)
            {
                queryString = uri.Query;
            }
            else
            {
                queryString = hasQueryString == -1 ? "" : uri.OriginalString.Substring(hasQueryString);
            }

            var values = queryString.Replace("?", "").Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                key => key.Substring(0, key.IndexOf('=')),
                value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters)
            {
                if (!p.ContainsKey(existing.Key))
                {
                    p.Add(existing);
                }
            }

            var query = string.Join(
                "&",
                p.Where(param => !string.IsNullOrWhiteSpace(param.Value))
                    .Select(kvp => FirstCharacterToLower(kvp.Key) + "=" + Uri.EscapeDataString(kvp.Value)));
            if (uri.IsAbsoluteUri)
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Query = query
                };
                return uriBuilder.Uri;
            }

            return new Uri(uriWithoutQuery + "?" + query, UriKind.Relative);
        }

        /// <summary>
        /// Merge a dictionary of values with an existing <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        public static string ApplyParameters(this string uri, IDictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException($"{nameof(uri)} can't be null");

            if (parameters == null || !parameters.Any())
            {
                return uri;
            }

            // to prevent values being persisted across requests
            // use a temporary dictionary which combines new and existing parameters
            IDictionary<string, string> p = new Dictionary<string, string>(parameters);

            var hasQueryString = uri.IndexOf("?", StringComparison.Ordinal);

            var uriWithoutQuery =
                hasQueryString == -1 ? uri : uri.Substring(0, hasQueryString);

            var queryString = hasQueryString == -1 ? "" : uri.Substring(hasQueryString);

            var values = queryString.Replace("?", "").Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                key => key.Substring(0, key.IndexOf('=')),
                value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters)
            {
                if (!p.ContainsKey(existing.Key))
                {
                    p.Add(existing);
                }
            }

            var query = string.Join(
                "&",
                p.Where(param => !string.IsNullOrWhiteSpace(param.Value))
                    .Select(kvp => FirstCharacterToLower(kvp.Key) + "=" + Uri.EscapeDataString(kvp.Value)));
            return $"{uriWithoutQuery}?{query}";
        }
    }
}
