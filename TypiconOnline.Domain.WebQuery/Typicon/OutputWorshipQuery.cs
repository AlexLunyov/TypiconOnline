using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputWorshipQuery : IQuery<Result<FilteredOutputWorshipExtended>>
    {
        public OutputWorshipQuery(int id, [NotNull] OutputFilter filter)
        {
            Id = id;
            Filter = filter ?? throw new ArgumentNullException(nameof(OutputFilter));
        }
        public int Id { get; }
        public OutputFilter Filter { get; }
    }
}
