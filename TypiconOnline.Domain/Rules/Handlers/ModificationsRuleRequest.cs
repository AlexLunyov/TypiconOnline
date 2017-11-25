using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ModificationsRuleRequest: ServiceRequestBase
    {
        public DayRule Caller;
        public DateTime Date;
        public int Priority;
        public bool IsLastName;
        public string ShortName;
        public bool AsAddition;
        public bool UseFullName;
    }
}
