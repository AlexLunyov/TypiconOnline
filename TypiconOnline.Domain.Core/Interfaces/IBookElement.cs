using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Core
{
    interface IBookElement<T> where T : DayElementBase
    {
        T GetElement();
        T GetElement(ITypiconSerializer serializer);
    }
}
