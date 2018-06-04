using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers.IntConverters;

namespace TypiconOnline.Domain.Rules.Handlers
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
