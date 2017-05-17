using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class Kathisma
    {
        public int Number { get; set; }
        public virtual List<Psalm> Psalms { get; set; }
    }
}
