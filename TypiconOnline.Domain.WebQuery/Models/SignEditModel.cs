using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class SignEditModel
    {
        public int Id { get; set; }
        public ItemText Name { get; set; } = new ItemText();
        public int? TemplateId { get; set; }
        public bool IsAddition { get; set; }
        public int? Number { get; set; }
        public int Priority { get; set; }
        public string RuleDefinition { get; set; }
        public string ModRuleDefinition { get; set; }
    }
}
