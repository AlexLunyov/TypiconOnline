using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Easter;

namespace TypiconOnline.Domain.Books
{
    public partial class BookStorage
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

        private BookStorage()
        {
            //Oktoikh = new List<OktoikhGlas>();
            //Evangelie = new List<EvangelieBook>();
            //Apostol = new List<ApostolBook>();
            //Psalter = new List<Kathisma>();
            //OldTestament = new List<OldTestamentBook>();
        }

        

        #region Singletone pattern

        public static BookStorage Instance
        {
            get { return Nested.instance; }
        }

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
