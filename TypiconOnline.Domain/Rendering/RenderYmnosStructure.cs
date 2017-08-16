using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rendering
{
    public class RenderYmnosStructure : RenderContainer
    {
        public YmnosStructureKind Kind { get; set; }

        public int Ihos { get; set; }
        /// <summary>
        /// "Глас" текст
        /// </summary>
        public string IhosText { get; set; }

        public RenderYmnosStructure(YmnosStructureRule rule, RuleHandlerBase handler)
        {
            if (rule == null || rule.CalculatedYmnosStructure == null) throw new ArgumentNullException("ymnosStructure");
            if (handler == null) throw new ArgumentNullException("handler");

            rule.ThrowExceptionIfInvalid();

            Kind = rule.YmnosStructureKind.Value;

            //groups
            for (int i = 0; i < rule.CalculatedYmnosStructure.Groups.Count; i++)
            {
                YmnosGroup group = rule.CalculatedYmnosStructure.Groups[i];

                RenderYmnosGroup item = new RenderYmnosGroup(group, handler);

                if (i == 0)
                {
                    Ihos = group.Ihos.Value;
                    IhosText = item.IhosText;
                }

                ChildElements.Add(item);
            }

            if (rule.CalculatedYmnosStructure.Doxastichon != null)
            {
                ChildElements.Add(new RenderYmnosGroup(rule.CalculatedYmnosStructure.Doxastichon, handler));
            }

            if (rule.CalculatedYmnosStructure.Theotokion != null)
            {
                ChildElements.Add(new RenderYmnosGroup(rule.CalculatedYmnosStructure.Theotokion, handler));
            }
        }
    }
}
