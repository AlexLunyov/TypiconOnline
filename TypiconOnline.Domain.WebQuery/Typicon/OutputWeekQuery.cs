using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputWeekQuery : IQuery<Result<FilteredOutputWeek>>
    {
        public OutputWeekQuery(int typiconId, DateTime date, OutputFilter filter)
        {
            TypiconId = typiconId;
            Date = date;
            Filter = filter ?? throw new ArgumentNullException(nameof(OutputFilter));
        }

        public int TypiconId { get; set; }
        public DateTime Date { get; set; }
        public OutputFilter Filter { get; set; }
    }
}
