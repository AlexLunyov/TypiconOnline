using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Typicon
{
    public class GetTypiconEntitiesResponse : ServiceResponseBase
    {
        public IEnumerable<TypiconEntity> TypiconEntities { get; set; }
    }
}
