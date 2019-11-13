using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayRuleModelBase<T> 
    {
        public int Id { get; set; }
        public List<T> DayWorships { get; set; } = new List<T>();
        public int TemplateId { get; set; }
        public string ModRuleDefinition { get; set; }
        public string RuleDefinition { get; set; }
    }
}
