namespace ModularMonolith.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string TrySubstring(this string value, int length)
        {
            if (value.Length <= length)
            {
                return value;
            }
            return value[..length];
        }
    }
}
