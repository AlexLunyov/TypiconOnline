using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconPublishedVersionQuery : IQuery<Result<TypiconVersion>>
    {
        public TypiconPublishedVersionQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }
    }
}
