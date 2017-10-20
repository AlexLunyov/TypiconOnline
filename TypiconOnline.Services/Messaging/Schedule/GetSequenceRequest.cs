using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetSequenceRequest : ServiceRequestBase
    {
        public int TypiconId { get; set; }
        public DateTime Date { get; set; }
    }
}
