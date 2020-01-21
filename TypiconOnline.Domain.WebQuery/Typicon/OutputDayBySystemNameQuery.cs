using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputDayBySystemNameQuery : IQuery<Result<FilteredOutputDay>>
    {
        public OutputDayBySystemNameQuery(string systemName, DateTime date, OutputFilter filter)
        {
            SystemName = systemName;
            Date = date;
            Filter = filter ?? throw new ArgumentNullException(nameof(OutputFilter));
        }
        public string SystemName { get; }
        public DateTime Date { get; }
        public OutputFilter Filter { get; }
    }
}
