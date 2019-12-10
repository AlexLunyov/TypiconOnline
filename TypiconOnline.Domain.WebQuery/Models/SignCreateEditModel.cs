using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class SignCreateEditModel : TemplateRuleModelBase
    {
        public ItemText Name { get; set; } = new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"));
        public bool IsAddition { get; set; }
        public int? PrintTemplateId { get; set; }
        public int Priority { get; set; } = 5;
    }
}
