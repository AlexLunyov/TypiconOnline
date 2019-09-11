using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputDayQuery : IQuery<Result<FilteredOutputDay>>
    {
        public OutputDayQuery(int typiconId, DateTime date, OutputFilter filter)
        {
            TypiconId = typiconId;
            Date = date;
            Filter = filter ?? throw new ArgumentNullException(nameof(OutputFilter));
        }
        public int TypiconId { get; }
        public DateTime Date { get; }
        public OutputFilter Filter { get; }
    }
}
