using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Easter
{
    public interface IEasterContext
    {
        DateTime GetCurrentEaster(int year);

        IEnumerable<EasterItem> GetAll();
    }
}
