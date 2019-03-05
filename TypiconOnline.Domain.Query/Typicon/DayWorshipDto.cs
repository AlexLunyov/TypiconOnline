using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class DayWorshipDto
    {
        public virtual ItemTextStyled WorshipName { get; set; }
        public virtual ItemTextStyled WorshipShortName { get; set; }
        public bool UseFullName { get; set; }
        public bool IsCelebrating { get; set; }
        public string Definition { get; set; }
        //public DayContainer DayContainer { get; set; }
    }
}