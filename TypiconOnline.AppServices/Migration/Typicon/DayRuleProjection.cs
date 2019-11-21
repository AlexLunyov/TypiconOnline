using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    public abstract class DayRuleProjection: TemplateHavingRuleProjection
    {
        public bool IsAddition { get; set; }
        public List<(int WorshipId, int Order)> DayWorships { get; set; }
    }
}
