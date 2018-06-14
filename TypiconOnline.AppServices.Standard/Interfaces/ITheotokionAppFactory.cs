using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITheotokionAppFactory
    {
        TheotokionApp Create(TheotokionAppPlace source, int ihos, DayOfWeek day, string stringDefinition);
    }
}
