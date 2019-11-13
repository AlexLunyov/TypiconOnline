using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TriodionRuleGridModel : IGridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public int DaysFromEaster { get; set; }
        public bool IsTransparent { get; set; }
        public bool HasModRuleDefinition { get; set; }
        public bool HasRuleDefinition { get; set; }
    }
}
