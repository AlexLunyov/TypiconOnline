using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Typicon.Factories
{
    public static class TypiconEntityFactory
    {
        public static TypiconEntity Create()
        {
            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Типикон",
                DefaultLanguage = "cs-ru"
            };

            return typiconEntity;
        }
    }
}
