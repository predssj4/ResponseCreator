﻿using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using ResponseCreator.Extensions.String;

namespace ResponseCreator.Translations
{
    public sealed class ValidationMessagesManager : IValidationMessagesManager
    {
        private static IValidationMessagesManager instance = new ValidationMessagesManager();

        public static IValidationMessagesManager GetValidationMessagesManager => instance;

        public string GetValidationMessageByKey(string key)
        {
            return GetValidationMessageWithCulture(key);
        }

        public string GetValidationMessageByKey(string key, params object[] paramsToInsertIntoMessage)
        {
            return GetValidationMessageWithCulture(key).ReFormat(paramsToInsertIntoMessage);
        }

        private string GetValidationMessageWithCulture(string key)
        {
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;

            switch (currentCulture.TwoLetterISOLanguageName)
            {
                case "pl":
                    return TranslationDictionaries.PolishTranslations[key];
                case "en":
                default:
                    return TranslationDictionaries.EnglishUsTranslations[key];
            }
        }
    }

    public class ValidationMessagesKeys
    {
        public const string NoLongerThen = nameof(NoLongerThen);
        public const string NoShorterThen = nameof(NoShorterThen);
        public const string Required = nameof(Required);
    }
}