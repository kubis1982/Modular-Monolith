namespace ModularMonolith.Shared.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string WithLeadingSlash(this string pattern)
        {
            if (pattern.Length == 0)
            {
                return pattern;
            }
            if (pattern[0] != '/' && !(pattern.StartsWith("http://") || pattern.StartsWith("https://")))
            {
                pattern = '/' + pattern;
            }
            if (pattern[^1] == '/')
            {
                pattern = new string(pattern.Substring(0, pattern.Length - 1));
            }
            return pattern;
        }
    }
}
