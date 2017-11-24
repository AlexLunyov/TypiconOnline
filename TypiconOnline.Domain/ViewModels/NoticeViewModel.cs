using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class NoticeViewModel : WorshipRuleViewModel
    {
        public NoticeViewModel(WorshipRule item, RuleHandlerBase handler) : base(item, handler)
        {
        }
    }
}
