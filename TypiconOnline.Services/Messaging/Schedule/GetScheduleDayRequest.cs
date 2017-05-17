using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleDayRequest
    {
        public TypiconEntity TypiconEntity { get; set; }
        public virtual DateTime Date { get; set; }
        
        //TODO: ???? нужен ли?
        public IRuleHandler RuleHandler { get; set; }
        public HandlingMode Mode { get; set; }

        //TODO: реализовать. Либо можно будет обойтись только RuleHandler ???
        public List<GetScheduleRequestParameter> CustomParameters { get; set; }
    }
}
