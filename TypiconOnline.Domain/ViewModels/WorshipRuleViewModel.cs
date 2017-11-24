using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class WorshipRuleViewModel : ContainerViewModel
    {
        public WorshipRuleViewModel() { }

        public WorshipRuleViewModel(WorshipRule service, RuleHandlerBase handler) : base(service, handler)
        {
            Text = service.Name;
            Time = service.Time.Expression;
            IsDayBefore = service.IsDayBefore;
            AdditionalName = service.AdditionalName;
        }

        public string Time { get; set; }
        public bool IsDayBefore { get; set; }
        public string AdditionalName { get; set; }
    }
}
