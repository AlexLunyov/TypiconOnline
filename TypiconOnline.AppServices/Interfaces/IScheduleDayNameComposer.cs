using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayNameComposer
    {
        ItemText Compose(DateTime date, int seniorRulePriority, IReadOnlyList<DayWorship> dayWorships);
        ItemTextUnit GetLocalizedWeekName(int typiconId, DateTime date, string language);
    }
}
