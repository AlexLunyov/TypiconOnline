using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.ViewModels.Messaging
{
    public class AppendViewModelOdiRequest
    {
        public Odi Odi { get; set; }
        public bool IsLastKanonas { get; set; }
        public bool IsOdi8 { get; set; }
        public ItemText DefaultChorus { get; set; }
    }
}
