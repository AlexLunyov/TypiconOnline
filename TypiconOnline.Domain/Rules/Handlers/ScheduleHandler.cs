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
        ViewModelRoot model = new ViewModelRoot();

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

                ClearResult();
            }
        }

        public override void ClearResult()
        {
            model.Worships.Clear();
        }

        public override bool Execute(ICustomInterpreted element)
        {
            if (element is WorshipRule w)
            {
                model.Worships.Add(new WorshipRuleViewModel(w, Settings.Language.Name));

                return true;
            }

            return false;
        }

        public virtual ViewModelRoot GetResult() => model;
    }
}
