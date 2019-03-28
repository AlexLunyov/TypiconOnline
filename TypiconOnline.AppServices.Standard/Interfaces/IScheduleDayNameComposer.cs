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
        ItemTextUnit Compose(DateTime date, int seniorRulePriority, IReadOnlyList<DayWorship> dayWorships, LanguageSettings language);
        ItemTextUnit GetWeekName(DateTime date, string language);
    }
}
