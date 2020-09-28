using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDataCalculator
    {
        Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest request);
    }
}
