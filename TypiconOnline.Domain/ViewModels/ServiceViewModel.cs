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
    public class ServiceViewModel : ContainerViewModel
    {
        public string Time;
        public bool IsDayBefore;
        public string AdditionalName;

        public ServiceViewModel() { }

        public ServiceViewModel(Service service, RuleHandlerBase handler)
        {
            CopyOnlyValues(service);

            foreach (RuleElement element in service.ChildElements)
            {
                if ((element is ICustomInterpreted) && handler.IsTypeAuthorized(element as ICustomInterpreted))
                {
                    ChildElements.Add(ViewModelFactory.CreateElement(element, handler));
                }
            }
        }

        public void CopyOnlyValues(Service service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("Service");
            }

            service.ThrowExceptionIfInvalid();

            Text = service.Name;
            Time = service.Time.Expression;
            IsDayBefore = service.IsDayBefore.Value;
            AdditionalName = service.AdditionalName;
        }
    }
}
