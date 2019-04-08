using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDataCalculator
    {
        ScheduleDataCalculatorResponse Calculate(ScheduleDataCalculatorRequest request);
    }
}
