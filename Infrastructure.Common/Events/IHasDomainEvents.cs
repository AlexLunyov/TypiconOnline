using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Events
{
    public interface IHasDomainEvents
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}
