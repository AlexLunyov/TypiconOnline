using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Easter
{
    public interface IEasterStorageFactory
    {
        IEasterStorage Create();
    }
}
