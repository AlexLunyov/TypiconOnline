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
    public class RenderServiceSequence : RenderContainer
    {
        public ServiceSequenceKind Kind { get; set; }

        public RenderServiceSequence(ServiceSequence rule, RuleHandlerBase handler)
        {
            if (rule == null) throw new ArgumentNullException("ServiceSequence");
            if (handler == null) throw new ArgumentNullException("handler");

            rule.ThrowExceptionIfInvalid();

            Kind = rule.ServiceSequenceKind.Value;

            foreach (RuleElement element in rule.ChildElements)
            {
                if ((element is ICustomInterpreted) && handler.IsTypeAuthorized(element as ICustomInterpreted))
                {
                    ChildElements.Add(RenderingFactory.CreateElement(element, handler));
                }
            }
        }
    }
}
