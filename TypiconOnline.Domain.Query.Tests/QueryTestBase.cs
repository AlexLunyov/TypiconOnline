using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Query.Tests
{
    public abstract class QueryTestBase
    {
        protected QueryTestBase()
        {
            Processor = DataQueryProcessorFactory.Create();
        }

        public IDataQueryProcessor Processor { get; }
    }
}
