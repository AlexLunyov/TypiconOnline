using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Books.Apostol
{
    public class ApostolBook
    {
        public virtual string Name { get; set; }
        public virtual List<ApostolChapter> Chapters { get; set; }
    }
}
