using System.Collections.Generic;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Typicon.Output;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputDayFactory
    {
        CreateOutputDayResponse Create(CreateOutputDayRequest req);

        CreateOutputDayResponse Create(IScheduleDataCalculator dataCalculator, CreateOutputDayRequest req);

        IEnumerable<OutputDay> CreateWeek(CreateOutputWeekRequest req);
    }
}
