using System;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Migration.Books
{
    [Serializable]
    public class MenologyWorshipProjection : WorshipProjectionBase
    {
        public string Date { get; set; }
        public string LeapDate { get; set; }
    }
}