using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Typicon
{
    public class UpdateTypiconVersionRequest: ServiceRequestBase
    {
        public TypiconVersion TypiconVersion { get; set; }
    }
}