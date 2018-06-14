using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class ScheduleDataCalculatorResponse : ServiceResponseBase
    {
        public TypiconRule Rule { get; set; }
        public RuleHandlerSettings Settings { get; set; }
    }
}
