//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TypiconOnline.Domain.Books.Apostol;

//namespace TypiconOnline.Domain.Books
//{
//    partial class BookStorage
//    {
//        public class ApostolClass
//        {
//            public virtual List<ApostolBook> ApostolBookSet { get; set; }

//            internal ApostolClass()
//            {
//                ApostolBookSet = new List<ApostolBook>();
//            }
//        }

//        #region Singletone pattern

//        public static ApostolClass Apostol
//        {
//            get { return NestedApostol.instance; }
//        }

//        private class NestedApostol
//        {
//            static NestedApostol()
//            {
//            }

//            internal static readonly ApostolClass instance = new ApostolClass();
//        }

//        #endregion
//    }
    
//}
