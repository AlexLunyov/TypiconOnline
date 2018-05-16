using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IMigrationManager
    {
        void Import();
        void Export();
    }
}
