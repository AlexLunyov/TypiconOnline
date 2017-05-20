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
        //??
        //public TypiconRule SeniorTypiconRule;
        //public TypiconRule JuniorTypiconRule;
        //public ModifiedRule AdditionModifiedRule;
        //??

        public List<TypiconRule> Rules;
        public HandlingMode Mode;
        public bool PutSeniorRuleNameToEnd = false;
        public string ShortName;
        public bool UseFullName = true;

        public RuleHandlerRequest()
        {
            Rules = new List<TypiconRule>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seniorTypiconRule">Главное правило для обработки</param>
        public RuleHandlerRequest(TypiconRule seniorTypiconRule) : this()
        {
            Rules.Add(seniorTypiconRule);
        }
    }
}
