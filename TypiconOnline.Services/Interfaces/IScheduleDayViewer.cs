using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Schedule;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayViewer
    {
        void Execute(ScheduleDay day);
    }
}
