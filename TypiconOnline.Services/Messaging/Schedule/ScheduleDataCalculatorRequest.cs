using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class ScheduleDataCalculatorRequest
    {
        public int TypiconId { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public CustomParamsCollection<IRuleApplyParameter> ApplyParameters { get; set; }
        public CustomParamsCollection<IRuleCheckParameter> CheckParameters { get; set; }
    }
}
