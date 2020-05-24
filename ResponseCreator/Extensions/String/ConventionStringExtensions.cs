using System.Text.RegularExpressions;

namespace ResponseCreator.Extensions.String
{
    internal static class ConventionStringExtensions
    {
        internal static string ToCamelCase(this string text)
        {
            text = text.Trim();

            text = UncapitaliseFirstChar(text);

            return text;
        }

        private static string RemoveAllButLastCapitalised(string text)
        {
            text = text.Substring(text.Length - 1);
            text = text.ToUpperInvariant();
            return text;
        }

        private static string UncapitaliseFirstChar(string nonSpacedText)
        {
            nonSpacedText = Regex.Replace(nonSpacedText, @"(^.)", EvaluateUncapitaliseString);
            return nonSpacedText;
        }

        private static string EvaluateUncapitaliseString(Match match)
        {
            return match.Groups[1].Value.ToLowerInvariant();
        }
    }
}
