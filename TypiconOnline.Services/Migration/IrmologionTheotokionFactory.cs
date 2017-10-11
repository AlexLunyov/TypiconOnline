using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Irmologion;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Migration
{
    public class IrmologionTheotokionFactory : IIrmologionTheotokionFactory
    {
        public IrmologionTheotokion Create(PlaceYmnosSource source, int ihos, DayOfWeek day, string stringDefinition)
        {
            return new IrmologionTheotokion()
            {
                Place = source,
                Ihos = ihos,
                DayOfWeek = day,
                StringDefinition = stringDefinition
            };
        }
    }
}
