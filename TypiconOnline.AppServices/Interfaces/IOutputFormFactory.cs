using System.Collections.Generic;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputFormFactory
    {
        OutputForm Create(CreateOutputFormRequest req);

        IEnumerable<OutputForm> CreateWeek(CreateOutputFormWeekRequest req);
    }
}
