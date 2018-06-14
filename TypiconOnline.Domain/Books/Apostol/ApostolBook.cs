using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Apostol
{
    public class ApostolBook
    {
        public virtual string Name { get; set; }
        public virtual List<ApostolChapter> Chapters { get; set; }
    }
}
