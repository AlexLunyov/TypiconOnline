using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class PrintWeekTemplateEditQuery : IQuery<Result<PrintWeekTemplateEditModel>>
    {
        public PrintWeekTemplateEditQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
