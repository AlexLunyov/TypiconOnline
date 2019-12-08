using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class PrintWeekTemplateModelQuery: IQuery<Result<PrintWeekTemplateModel>>
    {
        public PrintWeekTemplateModelQuery (int id)
        {
            TypiconId = id;
        }
        public int TypiconId { get; set; }
    }
}
