using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public interface IOktoikhService
    {
        DayService GetOktoikhDay(DateTime date);
        string GetSundayName(DateTime date);
        string GetSundayName(DateTime date, string stringToPaste);
        string GetWeekName(DateTime date, bool isShortName);
    }
}
