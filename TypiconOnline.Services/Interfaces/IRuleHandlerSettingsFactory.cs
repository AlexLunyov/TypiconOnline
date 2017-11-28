using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IRuleHandlerSettingsFactory
    {
        RuleHandlerSettings Create(GetRuleSettingsRequest request);
    }
}
