using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Typicon
{
    public class InsertTypiconEntityRequest: ServiceRequestBase
    {
        public TypiconEntity TypiconEntity { get; set; }
    }
}