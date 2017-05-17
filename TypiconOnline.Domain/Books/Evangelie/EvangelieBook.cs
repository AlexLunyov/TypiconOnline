using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Evangelie
{
    public class EvangelieBook
    {
        public virtual string Name { get; set; }
        public virtual List<EvangelieChapter> Chapters { get; set; }
    }
}
