using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOktoikhDayFileReader
    {
        string Read(int ihos, DayOfWeek dayOfWeek);
    }
}
