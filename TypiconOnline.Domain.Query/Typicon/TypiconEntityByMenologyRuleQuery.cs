using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByMenologyRuleQuery : TypiconEntityByChildQuery<MenologyRule>
    {
        public TypiconEntityByMenologyRuleQuery(int id) : base(id)
        {
        }
    }
}
