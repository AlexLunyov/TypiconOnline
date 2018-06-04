using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Books.OldTestament
{
    public class OldTestamentBook
    {
        public string Name { get; set; }
        public virtual List<OldTestamentChapter> Chapters { get; set; }
    }
}
