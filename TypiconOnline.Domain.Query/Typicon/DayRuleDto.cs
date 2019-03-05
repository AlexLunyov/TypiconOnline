using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class DayRuleDto : ITemplateHaving
    {
        public int Id { get; set; }
        public bool IsAddition { get; set; }
        public SignDto Template { get; set; }
        public IReadOnlyList<DayWorshipDto> DayWorships { get; set; }
        public string RuleDefinition { get; set; }
    }
}
