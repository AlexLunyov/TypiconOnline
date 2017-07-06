using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Evangelion
{
    public class EvangelionBook
    {
        public virtual string Name { get; set; }
        public virtual List<EvangelionChapter> Chapters { get; set; }
    }
}
