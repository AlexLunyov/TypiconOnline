﻿using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    /// <summary>
    /// кастомная настройка для класса KekragariaRule
    /// </summary>
    public class WorshipRuleCheckModeParameter : CheckParameterBase<WorshipRule>
    {
        public HandlingMode Mode { get; set; } = HandlingMode.All;

        protected override bool InnerCheck(WorshipRule element)
        {
            return ((Mode == HandlingMode.All) 
                    || ((Mode == HandlingMode.DayBefore) && (element.Mode == WorshipMode.DayBefore))
                    || ((Mode == HandlingMode.ThisDay) && (element.Mode != WorshipMode.DayBefore)));
        }
    }
}
