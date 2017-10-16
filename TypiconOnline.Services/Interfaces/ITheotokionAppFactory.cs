using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITheotokionAppFactory
    {
        TheotokionApp Create(PlaceYmnosSource source, int ihos, DayOfWeek day, string stringDefinition);
    }
}
