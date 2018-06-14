using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Common
{
    /// <summary>
    /// Настройки языка для обработки правил
    /// </summary>
    public class LanguageSettings
    {
        /// <summary>
        /// Наименование
        /// </summary>
        /// <example>cs-ru</example>
        public string Name { get; set; }
        public IIntConverter IntConverter { get; set; }
    }
}
