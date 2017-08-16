using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rendering
{
    public static class RenderingFactory
    {
        public static RenderElement CreateElement(RuleElement element, RuleHandlerBase handler)
        {
            RenderElement outputEl = null;

            if (element is TextHolder)
            {
                outputEl = new RenderTextHolder((element as TextHolder), handler);
            }
            else if (element is ServiceSequence)
            {
                outputEl = new RenderServiceSequence((element as ServiceSequence), handler);
            }
            else if (element is YmnosStructureRule)
            {
                outputEl = new RenderYmnosStructure((element as YmnosStructureRule), handler);
            }

            return outputEl;
        }
    }
}
