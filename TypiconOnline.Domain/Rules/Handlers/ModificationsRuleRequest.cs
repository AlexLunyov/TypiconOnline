using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ModificationsRuleRequest
    {
        public TypiconRule Caller;
        public DateTime Date;
        public int Priority;
        public bool IsLastName;
        public string ShortName;
        public bool AsAddition;
        public bool UseFullName;
    }
}
