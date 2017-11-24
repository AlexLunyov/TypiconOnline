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
    public class ServiceSequenceViewModel : ContainerViewModel
    {
        public ServiceSequenceKind Kind { get; set; }

        public ServiceSequenceViewModel(ServiceSequence rule, IRuleHandler handler) : base(rule, handler)
        {
            Kind = rule.ServiceSequenceKind;
        }
    }
}
