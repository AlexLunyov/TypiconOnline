using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByTriodionRuleQuery : TypiconEntityByChildQuery<TriodionRule>
    {
        public TypiconEntityByTriodionRuleQuery(int id) : base(id)
        {
        }
    }
}
