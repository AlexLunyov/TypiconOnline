using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.WeekDayApp
{
    public interface IWeekDayAppContext
    {
        GetWeekDayResponse Get(GetWeekDayRequest request);
    }
}
