using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class MenologyRuleModelBase : DayRuleModelBase<MenologyDayWorshipModel>
    {
        public bool IsAddition { get; set; }
    }
}