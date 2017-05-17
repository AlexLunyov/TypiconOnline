using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class RuleHandlerRequest
    {
        public TypiconRule SeniorTypiconRule;
        public TypiconRule JuniorTypiconRule;
        public ModifiedRule AdditionModifiedRule;
        public HandlingMode Mode;
        public bool PutSeniorRuleNameToEnd = false;
        public string ShortName;
    }
}
