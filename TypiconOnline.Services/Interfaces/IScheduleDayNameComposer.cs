using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayNameComposer
    {
        ItemTextUnit Compose(DateTime date, int seniorRulePriority, ICollection<DayWorship> dayWorships, LanguageSettings language);
        ItemTextUnit GetWeekName(DateTime date, string language);
    }
}
