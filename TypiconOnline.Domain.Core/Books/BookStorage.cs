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
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Interfaces;

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

        public IEvangelionContext Evangelion { get; private set; }
        public IApostolContext Apostol { get; private set; }
        public IOldTestamentContext OldTestament { get; private set; }
        public IPsalterContext Psalter { get; private set; }
        public IOktoikhContext Oktoikh { get; private set; }
        public ITheotokionAppContext TheotokionApp { get; private set; }
        public IEasterContext Easters { get; private set; }
        public IKatavasiaContext Katavasia { get; private set; }
        public IWeekDayAppContext WeekDayApp { get; private set; }
        public IRulesExtractor RulesExtractor { get; private set; }

        private BookStorage()
        {
            //Oktoikh = new List<OktoikhGlas>();
            //Evangelie = new List<EvangelieBook>();
            //Apostol = new List<ApostolBook>();
            //Psalter = new List<Kathisma>();
            //OldTestament = new List<OldTestamentBook>();
        }

        public BookStorage(IEvangelionContext evangelionService, 
            IApostolContext apostolService,
            IOldTestamentContext oldTestamentService,
            IPsalterContext psalterService,
            IOktoikhContext oktoikhContext,
            ITheotokionAppContext theotokionApp,
            IEasterContext easterContext,
            IKatavasiaContext katavasia,
            IWeekDayAppContext weekDayApp,
            IRulesExtractor rulesExtractor)
        {
            Evangelion = evangelionService ?? throw new ArgumentNullException("evangelionService");
            Apostol = apostolService ?? throw new ArgumentNullException("apostolService");
            OldTestament = oldTestamentService ?? throw new ArgumentNullException("oldTestamentService");
            Psalter = psalterService ?? throw new ArgumentNullException("psalterService");
            Oktoikh = oktoikhContext ?? throw new ArgumentNullException("oktoikhContext");
            TheotokionApp = theotokionApp ?? throw new ArgumentNullException("theotokionApp");
            Easters = easterContext ?? throw new ArgumentNullException("easterContext");
            Katavasia = katavasia ?? throw new ArgumentNullException("katavasia");
            WeekDayApp = weekDayApp ?? throw new ArgumentNullException("weekDayApp");
            RulesExtractor = rulesExtractor ?? throw new ArgumentNullException("rulesExtractor");
        }

        #region Singletone pattern

        //public static BookStorage Instance { get; set; }
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
