using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayNameComposer
    {
        string Compose(RuleHandlerSettings settings, DateTime date);
    }
}
