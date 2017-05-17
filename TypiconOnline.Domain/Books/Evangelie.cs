using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Evangelie;

namespace TypiconOnline.Domain.Books
{
    partial class BookStorage
    {
        public class EvangelieClass
        {
            public virtual List<EvangelieBook> EvangelieBookSet { get; set; }

            internal EvangelieClass()
            {
                EvangelieBookSet = new List<EvangelieBook>();
            }
        }

        #region Singletone pattern

        public static EvangelieClass Evangelie
        {
            get { return NestedEvangelie.instance; }
        }

        private class NestedEvangelie
        {
            static NestedEvangelie()
            {
            }

            internal static readonly EvangelieClass instance = new EvangelieClass();
        }

        #endregion
    }
    
}
