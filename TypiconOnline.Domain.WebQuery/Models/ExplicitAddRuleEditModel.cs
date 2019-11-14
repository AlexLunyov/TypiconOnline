using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class ExplicitAddRuleEditModel : RuleModelBase
    {
        public DateTime Date { get; set; }
        public ModelMode Mode { get; set; }
    }
}
