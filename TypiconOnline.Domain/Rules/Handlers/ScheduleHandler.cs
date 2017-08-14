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
        public ScheduleHandler()//(RuleHandlerSettings request) : base(request)
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
                if ((_settings.Mode == HandlingMode.All) ||
                    ((_settings.Mode == HandlingMode.DayBefore) && ((element as Service).IsDayBefore.Value)) ||
                    ((_settings.Mode == HandlingMode.ThisDay) && (!(element as Service).IsDayBefore.Value)))
                {
                    if (_executingResult == null)
                    {
                        _executingResult = new ExecContainer();
                    }

                    _executingResult.ChildElements.Add(element as Service);
                }
            }
        }

        //public override void Initialize(RuleHandlerSettings settings)
        //{
        //    base.Initialize(settings);

        //    _executingResult = null;
        //}

        //public override RuleContainer GetResult()
        //{
        //    return _executingResult;
        //}
    }
}
