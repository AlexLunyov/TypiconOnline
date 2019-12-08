using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class DownloadPrintWeekTemplateQuery : IQuery<Result<FileDownloadResponse>>
    {
        public DownloadPrintWeekTemplateQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
