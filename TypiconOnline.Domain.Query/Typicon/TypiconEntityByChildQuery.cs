using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public abstract class TypiconEntityByChildQuery<T>: IQuery<Result<TypiconEntity>> where T: ITypiconVersionChild, new()
    {
        public TypiconEntityByChildQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
