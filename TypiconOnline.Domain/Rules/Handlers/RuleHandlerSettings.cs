using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Настройки для обработки правил
    /// </summary>
    public class RuleHandlerSettings
    {
        public RuleHandlerSettings()
        {
            LanguageSettings = LanguageSettingsFactory.Create("cs-ru");
        }

        /// <summary>
        /// Дополнение для текущего правила
        /// </summary>
        public RuleHandlerSettings Addition { get; set; }
        /// <summary>
        /// Правило для обработки
        /// </summary>
        public TypiconRule Rule { get; set; }
        public List<DayWorship> DayWorships { get; set; } = new List<DayWorship>();
        public OktoikhDay OktoikhDay { get; set; }
        /// <summary>
        /// Дата - параметр для интерпретации элементов правил
        /// </summary>
        public DateTime Date { get; set; }
        //public HandlingMode Mode { get; set; }
        //public string ShortName { get; set; }
        //public bool UseFullName { get; set; }
        /// <summary>
        /// Язык для обработки
        /// </summary>
        public string Language { get; set; }

        public LanguageSettings LanguageSettings { get; set; }

        public CustomParamsCollection<IRuleApplyParameter> ApplyParameters { get; set; } = new CustomParamsCollection<IRuleApplyParameter>();
        public CustomParamsCollection<IRuleCheckParameter> CheckParameters { get; set; } = new CustomParamsCollection<IRuleCheckParameter>();

        /// <summary>
        /// Применяет кастомные праметры к элементу, если таковые найдутся - соответствующие его типу
        /// </summary>
        public void ApplyCustomParameters(RuleElement element)
        {
            ApplyParameters?.ForEach(c => c.Apply(element));
        }

        public bool CheckCustomParameters(RuleElement element)
        {
            return (CheckParameters != null) ? CheckParameters.TrueForAll(c => c.Check(element)) : true;
        }
    }
}
