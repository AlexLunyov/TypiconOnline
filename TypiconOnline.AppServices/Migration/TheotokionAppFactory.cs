using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.TheotokionApp;

namespace TypiconOnline.AppServices.Migration
{
    public class TheotokionAppFactory : ITheotokionAppFactory
    {
        public TheotokionApp Create(TheotokionAppPlace source, int ihos, DayOfWeek day, string stringDefinition)
        {
            return new TheotokionApp()
            {
                Place = source,
                Ihos = ihos,
                DayOfWeek = day,
                Definition = stringDefinition
            };
        }
    }
}
