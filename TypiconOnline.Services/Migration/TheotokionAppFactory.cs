using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Migration
{
    public class TheotokionAppFactory : ITheotokionAppFactory
    {
        public TheotokionApp Create(PlaceYmnosSource source, int ihos, DayOfWeek day, string stringDefinition)
        {
            return new TheotokionApp()
            {
                Place = source,
                Ihos = ihos,
                DayOfWeek = day,
                StringDefinition = stringDefinition
            };
        }
    }
}
