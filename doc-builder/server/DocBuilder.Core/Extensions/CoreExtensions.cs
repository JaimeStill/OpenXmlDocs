using System.Text.RegularExpressions;

namespace DocBuilder.Core.Extensions
{
    public static class CoreExtensions
    {
        public static readonly string urlPattern = "[^a-zA-Z0-9-.]";

        public static string UrlEncode(this string url) => url.UrlEncode(urlPattern, "-");

        public static string UrlEncode(this string url, string pattern, string replace = "")
        {
            var safeUrl = Regex.Replace(url, @"\s", "-").ToLower();
            safeUrl = Regex.Replace(safeUrl, pattern, replace);
            return safeUrl;
        }

        public static IQueryable<T> SetupSearch<T>(
            this IQueryable<T> values,
            string search,
            Func<IQueryable<T>, string, IQueryable<T>> action,
            char split = '|'
        )
        {
            if (search.Contains(split))
            {
                var searches = search.Split(split);

                foreach (var s in searches)
                {
                    values = action(values, s.Trim());
                }

                return values;
            }
            else
                return action(values, search);
        }
    }
}