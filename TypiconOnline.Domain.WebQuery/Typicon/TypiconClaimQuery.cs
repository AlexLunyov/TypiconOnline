using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class TypiconClaimQuery : IQuery<Result<TypiconClaimModel>>
    {
        public TypiconClaimQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
