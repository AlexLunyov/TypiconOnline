using System.Collections.Generic;
using TypiconOnline.Domain.Books.OldTestament;

namespace TypiconOnline.Domain.Books
{
    partial class BookStorage
    {
        public class OldTestamentClass
        {
            public virtual List<OldTestamentBook> OldTestamentSet { get; set; }
            internal OldTestamentClass()
            {
                OldTestamentSet = new List<OldTestamentBook>();
            }
        }

        #region Singletone pattern

        public static OldTestamentClass OldTestament
        {
            get { return NestedOldTestament.instance; }
        }

        private class NestedOldTestament
        {
            static NestedOldTestament()
            {
            }

            internal static readonly OldTestamentClass instance = new OldTestamentClass();
        }

        #endregion
    }
    
}
