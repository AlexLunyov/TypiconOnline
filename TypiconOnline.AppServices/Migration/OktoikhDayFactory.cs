using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Oktoikh;

namespace TypiconOnline.AppServices.Migration
{
    public class OktoikhDayFactory : IOktoikhDayFactory
    {
        public OktoikhDay Create(int ihos, DayOfWeek day, string stringDefinition)
        {
            return new OktoikhDay()
            {
                Ihos = ihos,
                DayOfWeek = day,
                Definition = stringDefinition
            };
        }
    }
}
