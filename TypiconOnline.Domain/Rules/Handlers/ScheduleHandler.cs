using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ScheduleHandler : RuleHandlerBase
    {
        private RuleContainer _executingResult = null;

        public ScheduleHandler()//(RuleHandlerRequest request) : base(request)
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(Service),
                typeof(Notice)
            };
        }

        public override void Execute(ICustomInterpreted element)
        {
            if ((element is Service) || (element is Notice))
            {
                if ((Mode == HandlingMode.All) ||
                    ((Mode == HandlingMode.DayBefore) && ((element as Service).IsDayBefore.Value)) ||
                    ((Mode == HandlingMode.ThisDay) && (!(element as Service).IsDayBefore.Value)))
                {
                    if (_executingResult == null)
                    {
                        _executingResult = new ExecContainer();
                    }

                    _executingResult.ChildElements.Add(element as Service);
                }
            }
        }

        public override void Initialize(RuleHandlerRequest request)
        {
            base.Initialize(request);

            _executingResult = null;
        }

        public override RuleContainer GetResult()
        {
            return _executingResult;
        }
    }
}
