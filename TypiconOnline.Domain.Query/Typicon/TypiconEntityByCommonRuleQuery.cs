using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByCommonRuleQuery : TypiconEntityByChildQuery<CommonRule>
    {
        public TypiconEntityByCommonRuleQuery(int id) : base(id)
        {
        }
    }
}
