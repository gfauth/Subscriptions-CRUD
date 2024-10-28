using System.Text.RegularExpressions;

namespace SingleLog.Utils
{
    public static partial class Strings
    {
        public static string Clean(string dirtValue, bool cleanTag = false, bool cleanXml = false)
        {
            string cleanValue = dirtValue;

            if (cleanTag)
                cleanValue = Regex.Replace(cleanValue, @"<[^>]+>|&nbsp;", "");

            if (cleanXml)
            {
                cleanValue = Regex.Replace(cleanValue, @"<", "");
                cleanValue = Regex.Replace(cleanValue, @">", "");
            }

            cleanValue = Regex.Replace(cleanValue, @"\r\n?|\n|\r", " ");
            cleanValue = Regex.Replace(cleanValue, @"\t", " ");
            cleanValue = Regex.Replace(cleanValue, @"\s{2,}", " ");

            cleanValue = cleanValue.Trim();

            if (string.IsNullOrWhiteSpace(cleanValue))
                cleanValue = string.Empty;

            return cleanValue;
        }
    }
}
