using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class DownloadPrintDayTemplateQuery : IQuery<Result<FileDownloadResponse>>
    {
        public DownloadPrintDayTemplateQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
