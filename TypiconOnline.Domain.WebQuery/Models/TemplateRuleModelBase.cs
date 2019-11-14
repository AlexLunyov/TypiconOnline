using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class TemplateRuleModelBase : RuleModelBase
    {
        public int TemplateId { get; set; }
        public string ModRuleDefinition { get; set; }
    }
}
