using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.OldTestament
{
    public class OldTestamentBook
    {
        public string Name { get; set; }
        public virtual List<OldTestamentChapter> Chapters { get; set; }
    }
}
