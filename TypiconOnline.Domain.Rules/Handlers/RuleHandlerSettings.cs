using System;
using System.Linq;
using System.Collections.Generic;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Настройки для обработки правил
    /// </summary>
    public class RuleHandlerSettings
    {
        public RuleHandlerSettings()
        {
            Language = LanguageSettingsFactory.Create("cs-ru");
        }

        /// <summary>
        /// Дополнение для текущего правила
        /// </summary>
        public RuleHandlerSettings Addition { get; set; }
        /// <summary>
        /// Id версии Устава
        /// </summary>
        public int TypiconVersionId { get; set; }
        /// <summary>
        /// Десериализированная последовательность элементов
        /// </summary>
        public RootContainer RuleContainer { get; set; }

        public List<DayWorship> Menologies { get; set; } = new List<DayWorship>();
        public List<DayWorship> Triodions { get; set; } = new List<DayWorship>();

        public IReadOnlyList<DayWorship> AllWorships
        {
            get
            {
                return Triodions.Concat(Menologies).ToList();
            }
        }

        //TODO: заменить на DayContainer
        public OktoikhDay OktoikhDay { get; set; }
        /// <summary>
        /// Дата - параметр для интерпретации элементов правил
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Язык для обработки
        /// </summary>
        public LanguageSettings Language { get; set; }
        /// <summary>
        /// Номер Знака службы, который будет использовать для отображения в Расписании.
        /// Issue #6
        /// </summary>
        public int? SignNumber { get; set; }

        public CustomParamsCollection<IRuleApplyParameter> ApplyParameters { get; set; } = new CustomParamsCollection<IRuleApplyParameter>();
        public CustomParamsCollection<IRuleCheckParameter> CheckParameters { get; set; } = new CustomParamsCollection<IRuleCheckParameter>();

        /// <summary>
        /// Применяет кастомные праметры к элементу, если таковые найдутся - соответствующие его типу
        /// </summary>
        public void ApplyCustomParameters(IRuleElement element)
        {
            ApplyParameters?.ForEach(c => c.Apply(element));
        }

        public bool CheckCustomParameters(IRuleElement element)
        {
            return CheckParameters?.TrueForAll(c => c.Check(element)) ?? true;
        }
    }
}
