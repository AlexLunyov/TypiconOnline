using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleWeekViewer
    {
        void Execute(int typiconId, FilteredOutputWeek week);
    }

    public interface IScheduleWeekViewer<T> where T : class
    {
        T Execute(int typiconId, FilteredOutputWeek week);
    }
}
