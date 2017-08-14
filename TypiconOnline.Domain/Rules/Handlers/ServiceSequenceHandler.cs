using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ServiceSequenceHandler : RuleHandlerBase
    {
        public ServiceSequenceHandler() { }

        public override RuleHandlerSettings Settings
        {
            set
            {
                base.Settings = value;
            }
        }

        public override void Execute(ICustomInterpreted element)
        {
            throw new NotImplementedException();
        }

        public override RuleContainer GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
