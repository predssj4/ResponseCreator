using System;
using System.Collections.Generic;
using System.Text;

namespace ResponseCreator.Translations
{
    public class TranslationDictionaries
    {
        public static IDictionary<string, string> PolishTranslations = new Dictionary<string, string>()
        {
            {ValidationMessagesKeys.NoLongerThen, "Nie więcej niż {0} znak."},
            {ValidationMessagesKeys.NoShorterThen, "Nie mniej niż {0} znak."},
            {ValidationMessagesKeys.Required, "Pole jest wymagane."},
        };

        public static IDictionary<string, string> EnglishUsTranslations = new Dictionary<string, string>()
        {
            {ValidationMessagesKeys.NoLongerThen, "Should be no longer then {0} characters."},
            {ValidationMessagesKeys.NoShorterThen, "Should be no shorter then {0} characters."},
            {ValidationMessagesKeys.Required, "Field is required."},
        };
    }
}
