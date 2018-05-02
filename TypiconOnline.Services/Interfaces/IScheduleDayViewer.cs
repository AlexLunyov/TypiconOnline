using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayViewer<T> where T: class
    {
        T Execute(ScheduleDay day);
    }
}
