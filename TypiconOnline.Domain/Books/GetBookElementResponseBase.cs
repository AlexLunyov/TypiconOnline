using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books
{
    public abstract class GetBookElementResponseBase<T>: ServiceResponseBase where T : DayElementBase
    {
        public T BookElement { get; set; }
    }
}
