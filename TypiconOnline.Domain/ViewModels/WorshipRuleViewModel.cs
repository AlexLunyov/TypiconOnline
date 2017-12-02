using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class WorshipRuleViewModel : List<ViewModelItem>
    {
        public WorshipRuleViewModel() { }

        public WorshipRuleViewModel(WorshipRule worship)
        {
            Id = worship.Id;
            Name = worship.Name;
            Time = worship.Time.Expression;
            IsDayBefore = worship.IsDayBefore;
            AdditionalName = worship.AdditionalName;
        }
        public string Id { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public bool IsDayBefore { get; set; }
        public string AdditionalName { get; set; }
    }
}
