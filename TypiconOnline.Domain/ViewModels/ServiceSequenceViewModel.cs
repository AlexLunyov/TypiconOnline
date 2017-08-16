using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.ViewModels
{
    public class ServiceSequenceViewModel : ContainerViewModel
    {
        public ServiceSequenceKind Kind { get; set; }

        public ServiceSequenceViewModel(ServiceSequence rule, RuleHandlerBase handler)
        {
            if (rule == null) throw new ArgumentNullException("ServiceSequence");
            if (handler == null) throw new ArgumentNullException("handler");

            rule.ThrowExceptionIfInvalid();

            Kind = rule.ServiceSequenceKind.Value;

            foreach (RuleElement element in rule.ChildElements)
            {
                if ((element is ICustomInterpreted) && handler.IsTypeAuthorized(element as ICustomInterpreted))
                {
                    ChildElements.Add(ViewModelFactory.CreateElement(element, handler));
                }
            }
        }
    }
}
