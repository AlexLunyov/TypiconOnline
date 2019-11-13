using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyRuleGridModel : IGridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public bool IsAddition { get; set; }
        public string Date { get; set; }
        public string LeapDate { get; set; }
        public bool HasModRuleDefinition { get; set; }
        public bool HasRuleDefinition { get; set; }
    }
}
