using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Query.Typicon
{
    public interface ITemplateHaving
    {
        bool IsAddition { get; set; }
        SignDto Template { get; set; }
        string RuleDefinition { get; set; }
    }
}
