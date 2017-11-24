using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ScheduleHandler : RuleHandlerBase
    {
        private ContainerViewModel _executingResult;

        public ScheduleHandler()//(RuleHandlerSettings request) : base(request)
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(WorshipRule),
                typeof(Notice)
            };

        }

        public override RuleHandlerSettings Settings
        {
            get
            {
                return base.Settings;
            }

            set
            {
                base.Settings = value;
                _executingResult = null;
            }
        }

        public override void Execute(ICustomInterpreted element)
        {
            if ((element is WorshipRule) || (element is Notice))
            {
                if ((_settings.Mode == HandlingMode.All) ||
                    ((_settings.Mode == HandlingMode.DayBefore) && ((element as WorshipRule).IsDayBefore)) ||
                    ((_settings.Mode == HandlingMode.ThisDay) && (!(element as WorshipRule).IsDayBefore)))
                {
                    //if (_executingResult == null)
                    //{
                    //    _executingResult = new ContainerViewModel();
                    //}

                    WorshipRuleViewModel renderService = new WorshipRuleViewModel(element as WorshipRule, this);
                    //renderService.CopyOnlyValues(element as Service);

                    ExecutingResult.ChildElements.Add(renderService);
                }
            }
        }

        protected ContainerViewModel ExecutingResult
        {
            get
            {
                if (_executingResult == null)
                {
                    _executingResult = new ContainerViewModel();
                }
                return _executingResult;
            }
        }

        //public override void Initialize(RuleHandlerSettings settings)
        //{
        //    base.Initialize(settings);

        //    _executingResult = null;
        //}

        public virtual ContainerViewModel GetResult()
        {
            return _executingResult;
        }
    }
}
