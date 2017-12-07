using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;

namespace TypiconOnline.AppServices.Messaging.Books
{
    public class InsertPsalmRequest
    {
        public Psalm Psalm { get; set; }
    }
}