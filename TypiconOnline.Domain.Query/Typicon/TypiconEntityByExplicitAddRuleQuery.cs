using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByExplicitAddRuleQuery : TypiconEntityByChildQuery<ExplicitAddRule>
    {
        public TypiconEntityByExplicitAddRuleQuery(int id) : base(id)
        {
        }
    }
}
