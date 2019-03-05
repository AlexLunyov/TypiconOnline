using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain
{
    interface IBookElement<out T> where T : DayElementBase
    {
        T GetElement(ITypiconSerializer serializer);
    }
}
