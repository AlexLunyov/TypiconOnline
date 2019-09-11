using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleDayResponse: ServiceResponseBase
    {
        public OutputDay Day { get; set; }
    }
}
