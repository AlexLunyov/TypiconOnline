using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
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
