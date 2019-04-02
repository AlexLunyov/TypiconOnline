using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ScheduleHandler : RuleHandlerBase
    {
        List<OutputWorship> collection = new List<OutputWorship>();

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
            collection.Clear();
        }

        public override bool Execute(ICustomInterpreted element)
        {
            if (element is WorshipRule w)
            {
                collection.Add(new OutputWorship(w));

                return true;
            }

            return false;
        }

        public virtual ICollection<OutputWorship> GetResult() => collection;
    }
}
