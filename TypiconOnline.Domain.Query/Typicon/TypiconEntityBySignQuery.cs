using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityBySignQuery : TypiconEntityByChildQuery<Sign>
    {
        public TypiconEntityBySignQuery(int id) : base(id)
        {
        }
    }
}
