namespace ResponseCreator.Extensions.String
{
    internal static class StringExtensionsForValidation
    {
        internal static string ReFormat(this string target, params object[] paramsToInsert)
        {
            return string.Format(target, paramsToInsert);
        }
    }
}
