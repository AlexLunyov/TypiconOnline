using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Psalter
{
    public interface IPsalterContext
    {
        GetPsalmResponse Get(GetPsalmRequest request);
    }
}
