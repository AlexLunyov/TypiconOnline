using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IDayContainerFactory
    {
        DayContainer Create();
    }
}
