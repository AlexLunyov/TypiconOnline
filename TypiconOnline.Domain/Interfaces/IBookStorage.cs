using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IBookStorage
    {
        IEvangelionContext CreateEvangelionContext();
        IApostolContext CreateApostolContext();
        IOldTestamentContext CreateOldTestamentContext();
        IPsalterContext CreatePsalterContext();
        IOktoikhContext CreateOktoikhContext();
        ITheotokionAppContext CreateTheotokionAppContext();
        IEasterContext CreateEastersContext();
        IKatavasiaContext CreateKatavasiaContext();
    }
}
