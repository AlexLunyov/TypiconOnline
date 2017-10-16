using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;

namespace TypiconOnline.Domain.Books
{
    public class BookStorage
    {
        //Октоих
        //public virtual List<OktoikhGlas> Oktoikh { get; set; }

        //Евангелие EvangelieBook
        //public virtual List<EvangelieBook> Evangelie { get; set; }

        //Апостол
        //public virtual List<ApostolBook> Apostol{ get; set; }

        //Псалтирь
        //public virtual List<Kathisma> Psalter { get; set; }

        //Ветхий Завет 
        //public virtual List<OldTestamentBook> OldTestament { get; set; }

        public IEvangelionService Evangelion { get; private set; }
        public IApostolService Apostol { get; private set; }
        public IOldTestamentService OldTestament { get; private set; }
        public IPsalterService Psalter { get; private set; }
        public IOktoikhService Oktoikh { get; private set; }
        public ITheotokionAppContext TheotokionApp { get; private set; }
        public IEasterService Easters { get; private set; }

        private BookStorage()
        {
            //Oktoikh = new List<OktoikhGlas>();
            //Evangelie = new List<EvangelieBook>();
            //Apostol = new List<ApostolBook>();
            //Psalter = new List<Kathisma>();
            //OldTestament = new List<OldTestamentBook>();
        }

        public BookStorage(IEvangelionService evangelionService, 
            IApostolService apostolService,
            IOldTestamentService oldTestamentService,
            IPsalterService psalterService,
            IOktoikhService oktoikhService,
            ITheotokionAppContext theotokionApp,
            IEasterService easterService)
        {
            if (evangelionService == null) throw new ArgumentNullException("evangelionService");
            if (apostolService == null) throw new ArgumentNullException("apostolService");
            if (oldTestamentService == null) throw new ArgumentNullException("oldTestamentService");
            if (psalterService == null) throw new ArgumentNullException("psalterService");
            if (oktoikhService == null) throw new ArgumentNullException("oktoikhService");
            if (theotokionApp == null) throw new ArgumentNullException("irmologionService");
            if (easterService == null) throw new ArgumentNullException("irmologionService");

            Evangelion = evangelionService;
            Apostol = apostolService;
            OldTestament = oldTestamentService;
            Psalter = psalterService;
            Oktoikh = oktoikhService;
            TheotokionApp = theotokionApp;
            Easters = easterService;
        }

        #region Singletone pattern

        public static BookStorage Instance { get; set; }
        //{
        //    get { return Nested.instance; }
        //}

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly BookStorage instance = new BookStorage();
        }

        #endregion
    }
}
