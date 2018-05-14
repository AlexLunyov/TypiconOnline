using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Абстрактный класс для правил дял песнопений
    /// </summary>
    public abstract class YmnosRuleBase : RuleExecutable, ICalcStructureElement
    {
        public YmnosRuleBase(string name) : base(name) { }

        /// <summary>
        /// Тип песнопения (общий, славник, богородичен...)
        /// </summary>
        public YmnosRuleKind Kind { get; set; } = YmnosRuleKind.YmnosRule;

        public abstract DayElementBase Calculate(RuleHandlerSettings settings);
    }
}
