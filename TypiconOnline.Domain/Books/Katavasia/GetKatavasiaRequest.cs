using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Books.Katavasia
{
    public class GetKatavasiaRequest
    {
        public string Name { get; set; }
        public ITypiconSerializer Serializer { get; set; }
    }
}