using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityByVariableQuery : TypiconEntityByChildQuery<TypiconVariable>
    {
        public TypiconEntityByVariableQuery(int id) : base(id)
        {
        }
    }
}
