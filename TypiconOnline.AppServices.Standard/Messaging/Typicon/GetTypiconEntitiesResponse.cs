using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Typicon
{
    public class GetTypiconEntitiesResponse : ServiceResponseBase
    {
        public IEnumerable<TypiconVersion> TypiconEntities { get; set; }
    }
}
