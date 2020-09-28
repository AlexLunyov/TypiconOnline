using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputWeekBySystemNameQuery : IQuery<Result<(int, FilteredOutputWeek)>>
    {
        public OutputWeekBySystemNameQuery(string systemName, DateTime date, int weekCount, OutputFilter filter)
        {
            SystemName = systemName;
            Date = date;
            WeekCount = weekCount;
            Filter = filter ?? throw new ArgumentNullException(nameof(OutputFilter));
        }

        public string SystemName { get; set; }
        public DateTime Date { get; set; }
        public int WeekCount { get; set; }
        public OutputFilter Filter { get; set; }
    }
}
