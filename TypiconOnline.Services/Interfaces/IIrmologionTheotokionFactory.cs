using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Irmologion;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IIrmologionTheotokionFactory
    {
        IrmologionTheotokion Create(PlaceYmnosSource source, int ihos, DayOfWeek day, string stringDefinition);
    }
}
