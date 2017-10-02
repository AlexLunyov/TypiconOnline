using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IDayServiceFactory
    {
        DayService Create();
    }
}
