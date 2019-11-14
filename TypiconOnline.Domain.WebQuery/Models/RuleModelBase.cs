using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class RuleModelBase
    {
        public int Id { get; set; }
        public string RuleDefinition { get; set; }
    }
}
