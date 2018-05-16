using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.WeekDayApp;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IWeekDayAppFactory
    {
        WeekDayApp Create(string name, string definition);
    }
}
