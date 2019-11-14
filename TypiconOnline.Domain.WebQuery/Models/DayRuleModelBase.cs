using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayRuleModelBase<T> : TemplateRuleModelBase
    {
        public List<T> DayWorships { get; set; } = new List<T>();
    }
}
