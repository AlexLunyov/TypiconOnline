using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class Psalm : BookElementBase<BookReading>, IPsalterElement, IAggregateRoot
    {
        private BookReading reading;
        public Psalm() { }

        public virtual int Number { get; set; }

        public override BookReading GetElement(ITypiconSerializer serializer)
        {
            if (reading == null)
            {
                reading = base.GetElement(serializer) ?? new BookReading();
            }

            return reading;
        }
    }
}
