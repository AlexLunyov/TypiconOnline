using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class ServiceViewModel : ContainerViewModel
    {
        public ServiceViewModel() { }

        public ServiceViewModel(Service service, RuleHandlerBase handler) : base(service, handler)
        {
            if (service == null) throw new ArgumentNullException("Service");

            service.ThrowExceptionIfInvalid();

            Text = service.Name;
            Time = service.Time.Expression;
            IsDayBefore = service.IsDayBefore.Value;
            AdditionalName = service.AdditionalName;
        }

        public string Time { get; set; }
        public bool IsDayBefore { get; set; }
        public string AdditionalName { get; set; }
    }
}
