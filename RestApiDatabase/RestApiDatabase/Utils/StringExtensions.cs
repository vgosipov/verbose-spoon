namespace RestApiDatabase.Utils
{
    public static class StringExtensions
    {
        public static string ReplaceSingleQuote(this string source)
        {
            if (source is null) 
                return null;
            return source.Replace("\'", "''");
        }
    }
}
