using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class YmnosStructureRuleHandler : RuleHandlerBase
    {
        protected ExecContainer _executingResult;

        public YmnosStructureRuleHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(YmnosRule),
            };

            ClearResult();
        }

        public override void ClearResult()
        {
            _executingResult = null;
        }

        public override bool Execute(ICustomInterpreted element)
        {
            if (element is YmnosRule)
            {
                if (_executingResult == null)
                {
                    _executingResult = new ExecContainer();
                }

                _executingResult.ChildElements.Add(element as YmnosRule);

                return true;
            }
            return false;
        }

        public ExecContainer GetResult()
        {
            return _executingResult;
        }

        //public override void Initialize(RuleHandlerSettings request)
        //{
            
        //}
    }
}
