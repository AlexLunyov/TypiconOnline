using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Evangelion;

namespace TypiconOnline.Domain.Books
{
    partial class BookStorage
    {
        public class EvangelionClass
        {
            public virtual List<EvangelionBook> EvangelionBookSet { get; set; }

            internal EvangelionClass()
            {
                EvangelionBookSet = new List<EvangelionBook>();
            }
        }

        #region Singletone pattern

        public static EvangelionClass Evangelion
        {
            get { return NestedEvangelion.instance; }
        }

        private class NestedEvangelion
        {
            static NestedEvangelion()
            {
            }

            internal static readonly EvangelionClass instance = new EvangelionClass();
        }

        #endregion
    }
    
}
