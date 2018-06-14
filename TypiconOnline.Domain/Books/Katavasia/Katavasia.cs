using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Katavasia
{
    public class Katavasia : BookElementBase<Kanonas>, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
