using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Infrastructure.Common.Events
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Result Execute(TDomainEvent domainEvent);
    }
}
