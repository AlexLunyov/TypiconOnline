using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public abstract class RuleHandlerBase<T> : RuleHandlerBase where T : class
    {
        public abstract T GetResult();
    }
}
