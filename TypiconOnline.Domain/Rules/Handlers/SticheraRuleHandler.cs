using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class YmnosStructureRuleHandler : RuleHandlerBase
    {

        public YmnosStructureRuleHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(YmnosRule),
            };

            _executingResult = null;
        }

        public override void Execute(ICustomInterpreted element)
        {
            if (element is YmnosRule)
            {
                if (_executingResult == null)
                {
                    _executingResult = new ExecContainer();
                }

                _executingResult.ChildElements.Add(element as YmnosRule);
            }
        }

        public override RuleContainer GetResult()
        {
            return _executingResult;
        }

        //public override void Initialize(RuleHandlerSettings request)
        //{
            
        //}
    }
}
