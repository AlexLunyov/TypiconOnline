using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain
{
    interface IBookElement<T> where T : DayElementBase
    {
        T GetElement();
        T GetElement(ITypiconSerializer serializer);
    }
}
