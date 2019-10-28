using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Books
{ 
    public class OktoikhDayEditQuery : IQuery<Result<OktoikhDayEditModel>>
    {
        public OktoikhDayEditQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
