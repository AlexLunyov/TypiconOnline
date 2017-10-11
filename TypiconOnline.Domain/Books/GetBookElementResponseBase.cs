using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books
{
    public abstract class GetBookElementResponseBase<T>: ServiceResponseBase where T : DayElementBase
    {
        public T BookElement { get; set; }
    }
}
