using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Typicon
{
    public class GetTypiconEntityResponse : ServiceResponseBase
    {
        public Domain.Typicon.TypiconEntity TypiconEntity { get; set; }
    }
}
