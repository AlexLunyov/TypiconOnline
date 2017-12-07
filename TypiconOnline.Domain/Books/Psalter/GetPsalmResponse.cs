using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class GetPsalmResponse : ServiceResponseBase
    {
        public Psalm Psalm { get; set; }
    }
}