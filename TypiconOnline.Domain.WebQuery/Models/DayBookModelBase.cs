using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayBookModelBase: BookModelBase
    {
        public ItemTextStyled Name { get; set; }// = new ItemTextStyled(new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]")));
        public ItemText ShortName { get; set; }// = new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"));
        public bool IsCelebrating { get; set; }
        public bool UseFullName { get; set; }
    }
}