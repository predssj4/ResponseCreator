﻿using System.Text.RegularExpressions;

namespace ResponseCreator.Extensions.String
{
    internal static class ConventionStringExtensions
    {
        /// <summary>
        /// Converts text to camelCase.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        internal static string ToCamelCase(this string text)
        {
            text = text.Trim();

            // Remove all non-alphanumerics with following characters, and capitalise their following character.
            text = Regex.Replace(text, @"([^a-zA-Z\d]+[a-zA-Z])", RemoveAllButLastCapitalised);

            // Remove any remaining non-alphanumerics (i.e. ones that don't have any following characters).
            text = Regex.Replace(text, @"([^a-zA-Z\d])", string.Empty);

            // Un-capitalise first character.
            text = UncapitaliseFirstChar(text);

            return text;
        }

        /// <summary>
        /// Converts text to PascalCase.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        internal static string ToPascalCase(this string text)
        {
            text = text.Trim();

            // Remove all non-alphanumerics with following characters, and capitalise their following character.
            text = Regex.Replace(text, @"([^a-zA-Z\d]+[a-zA-Z])", RemoveAllButLastCapitalised);

            // Remove any remaining non-alphanumerics (i.e. ones that don't have any following characters).
            text = Regex.Replace(text, @"([^a-zA-Z\d])", string.Empty);

            // Capitalise first character.
            text = CapitaliseFirstChar(text);

            return text;
        }

        /// <summary>
        /// Converts text to lower_snake_case.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        internal static string ToLowerSnakeCase(this string text)
        {
            text = text.Trim();

            // Replace all upper-case characters with underscore and a lower-case version of the same character.
            text = Regex.Replace(text, @"\.?([A-Z])", "_$1");

            // Replace all non-alphanumerics with underscores.
            text = Regex.Replace(text, @"([^a-zA-Z\d]+)", "_");

            // Remove vestigial leading underscore that may be created from previous steps.
            text = Regex.Replace(text, @"^_", string.Empty);

            // Remove vestigial trailing underscore that may be created from previous steps.
            text = Regex.Replace(text, @"_$", string.Empty);

            // Convert all characters to lower-case.
            text = text.ToLowerInvariant();

            return text;
        }

        /// <summary>
        /// Converts text to _underscoreCamelCase.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        internal static string ToUnderscoreCamelCase(this string text)
        {
            text = text.Trim();

            // Remove all non-alphanumerics with following characters, and capitalise their following character.
            text = Regex.Replace(text, @"([^a-zA-Z\d]+[a-zA-Z])", RemoveAllButLastCapitalised);

            // Remove any remaining non-alphanumerics (i.e. ones that don't have any following characters).
            text = Regex.Replace(text, @"([^a-zA-Z\d])", string.Empty);

            // Un-capitalise first character.
            text = UncapitaliseFirstChar(text);

            text = string.Concat("_", text);

            return text;
        }

        private static string RemoveAllButLastCapitalised(Match match)
        {
            return RemoveAllButLastCapitalised(match.Groups[1].Value);
        }

        private static string RemoveAllButLastCapitalised(string text)
        {
            text = text.Substring(text.Length - 1);
            text = text.ToUpperInvariant();
            return text;
        }

        private static string CapitaliseFirstChar(string nonSpacedText)
        {
            nonSpacedText = Regex.Replace(nonSpacedText, @"(^.)", EvaluateCapitaliseString);
            return nonSpacedText;
        }

        private static string EvaluateCapitaliseString(Match match)
        {
            return match.Groups[1].Value.ToUpperInvariant();
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
