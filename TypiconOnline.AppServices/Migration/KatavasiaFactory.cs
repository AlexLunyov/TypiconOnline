using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Katavasia;

namespace TypiconOnline.AppServices.Migration
{
    public class KatavasiaFactory : IKatavasiaFactory
    {
        public Katavasia Create(string name, string definition)
        {
            return new Katavasia()
            {
                Name = name,
                Definition = definition
            };
        }
    }
}
