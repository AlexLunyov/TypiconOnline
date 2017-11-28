using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    /// <summary>
    /// кастомная настройка для класса KekragariaRule
    /// </summary>
    public class WorshipRuleCustomParameter : CustomParameterBase<WorshipRule>
    {
        public HandlingMode Mode { get; set; } = HandlingMode.All;

        protected override void InnerApply(WorshipRule element)
        {
            element.ModeFromHandler = Mode;
        }
    }
}
