using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class IsNameUniqueQuery : IQuery<Result>
    {
        public IsNameUniqueQuery(string systemName)
        {
            SystemName = systemName;
        }

        public string SystemName { get; }
    }
}
