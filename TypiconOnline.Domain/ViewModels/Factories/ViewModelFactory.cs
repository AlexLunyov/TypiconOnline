using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public static class ViewModelFactory
    {
        public static ElementViewModel CreateElement(RuleElement element, RuleHandlerBase handler)
        {
            ElementViewModel outputEl = null;

            if (element is TextHolder)
            {
                outputEl = new TextHolderViewModel((element as TextHolder), handler);
            }
            else if (element is ServiceSequence)
            {
                outputEl = new ServiceSequenceViewModel((element as ServiceSequence), handler);
            }
            else if (element is YmnosStructureRule)
            {
                outputEl = new YmnosStructureViewModel((element as YmnosStructureRule), handler);
            }

            return outputEl;
        }
    }
}
