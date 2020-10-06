using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class SignCreateEditModel : TemplateRuleModelBase
    {
        public int TypiconId { get; set; }
        public string Name { get; set; }
        public bool IsAddition { get; set; }
        public int? PrintTemplateId { get; set; }
        public int Priority { get; set; } = 5;
    }
}
