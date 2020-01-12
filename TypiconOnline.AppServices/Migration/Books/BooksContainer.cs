using System;
using System.Collections.Generic;

namespace TypiconOnline.AppServices.Migration.Books
{
    [Serializable]
    public class BooksContainer
    {
        public BooksContainer()
        {
        }

        public List<TriodionWorshipProjection> Triodions { get; set; }
        public List<MenologyWorshipProjection> Menologies { get; set; }
    }
}