using ResponseCreator.Extensions.String;

namespace ResponseCreator
{
    public class KeyPrefixBuilder
    {
        private const string PrefixKeySeparator = "_";

        public static string CreateKey(string key)
        {
            return key.ToCamelCase();
        }

        public static string CreateKeyWithPrefix(string key, string prefix = null)
        {
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return CreateKey(key);
            }
            else
            {
                return Join(prefix, key);
            }
        }

        public static string JoinPrefixes(string first, string second)
        {
            return $"{first.ToCamelCase()}{PrefixKeySeparator}{second.ToCamelCase()}";
        }

        public static string Join(string first, string second)
        {
            return $"{first.ToCamelCase()}{PrefixKeySeparator}{second.ToCamelCase()}";
        }
    }
}