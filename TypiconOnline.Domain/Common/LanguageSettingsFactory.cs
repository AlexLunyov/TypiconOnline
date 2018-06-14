using TypiconOnline.Domain.Common.IntConverters;

namespace TypiconOnline.Domain.Common
{
    public class LanguageSettingsFactory
    {
        public static LanguageSettings Create(string language)
        {
            LanguageSettings languageSettings = new LanguageSettings() { Name = language };

            switch (language)
            {
                case "cs-cs":
                    languageSettings.IntConverter = new IntCsConverter();
                    break;
                default:
                    languageSettings.IntConverter = new IntConverter();
                    break;
            }

            return languageSettings;
        }
    }
}
