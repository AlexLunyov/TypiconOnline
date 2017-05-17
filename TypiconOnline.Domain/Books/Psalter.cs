using System.Collections.Generic;
using TypiconOnline.Domain.Books.Psalter;

namespace TypiconOnline.Domain.Books
{
    partial class BookStorage
    {
        public class PsalterClass
        {
            public virtual List<Kathisma> KathismaSet { get; set; }
            internal PsalterClass()
            {
                KathismaSet = new List<Kathisma>();
            }
        }

        #region Singletone pattern

        public static PsalterClass Psalter
        {
            get { return NestedPsalter.instance; }
        }

        private class NestedPsalter
        {
            static NestedPsalter()
            {
            }

            internal static readonly PsalterClass instance = new PsalterClass();
        }

        #endregion
    }
    
}
