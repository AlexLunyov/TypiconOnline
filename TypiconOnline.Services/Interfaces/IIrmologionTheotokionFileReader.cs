using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IIrmologionTheotokionFileReader 
    {
        string Read(PlaceYmnosSource place, int ihos, DayOfWeek dayOfWeek);
    }
}
