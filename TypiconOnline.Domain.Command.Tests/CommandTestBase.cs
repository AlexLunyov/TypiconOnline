using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Command.Tests
{
    public abstract class CommandTestBase
    {
        protected CommandTestBase()
        {
            CommandProcessor = CommandProcessorFactory.Create();
            QueryProcessor = DataQueryProcessorFactory.Create();
        }

        public ICommandProcessor CommandProcessor { get; }
        public IDataQueryProcessor QueryProcessor { get; }
    }
}
