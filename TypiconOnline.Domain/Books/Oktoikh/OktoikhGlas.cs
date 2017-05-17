using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public class OktoikhGlas
    {
        public int Number { get; set; }
        public virtual List<OktoikhDay> Days { get; set; }
    }
}
