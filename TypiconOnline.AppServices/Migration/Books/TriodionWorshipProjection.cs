using System;

namespace TypiconOnline.AppServices.Migration.Books
{
    [Serializable]
    public class TriodionWorshipProjection : WorshipProjectionBase
    {
        public int DaysFromEaster { get; set; }
    }
}