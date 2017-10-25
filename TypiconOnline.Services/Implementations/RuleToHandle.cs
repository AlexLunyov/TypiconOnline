using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Класс участвует в обработке последовательностей богослужений.
    /// Используется в ScheduleService
    /// </summary>
    public class RuleToHandle
    {
        /// <summary>
        /// Правило для обработки
        /// </summary>
        public RuleElement Rule { get; set; }
        /// <summary>
        /// Коллекция богослужебных текстов для обработки
        /// </summary>
        public List<DayWorship> DayServices { get; set; }
        /// <summary>
        /// Добавление к текущему правилу, Nullable
        /// </summary>
        public RuleToHandle Addition { get; set; }
    }
}
