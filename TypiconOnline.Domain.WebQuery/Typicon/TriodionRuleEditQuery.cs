using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class TriodionRuleEditQuery : IQuery<Result<TriodionRuleCreateEditModel>>
    {
        public TriodionRuleEditQuery(int id, string language)
        {
            Id = id;
            Language = language;
        }

        public int Id { get; }
        public string Language { get; }
    }
}
