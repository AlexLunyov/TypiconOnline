using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class CommonRuleServiceRequest: ServiceRequestBase
    {
        public string Key { get; set; }
        public IRuleHandler Handler { get; set; }
    }
}
