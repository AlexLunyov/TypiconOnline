using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Evangelion
{
    public class EvangelionBook
    {
        public virtual string Name { get; set; }
        public virtual List<EvangelionChapter> Chapters { get; set; }
    }
}
