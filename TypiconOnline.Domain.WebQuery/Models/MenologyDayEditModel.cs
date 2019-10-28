using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyDayEditModel
    {
        public int Id { get; set; }
        public DateTime? LeapDate { get; set; }
        public ItemTextStyled Name { get; set; }// = new ItemTextStyled(new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]")));
        public ItemText ShortName { get; set; }// = new ItemText(new ItemTextUnit("cs-ru", "[Новое значение]"));
        public bool IsCelebrating { get; set; }
        public bool UseFullName { get; set; }
        public string Definition { get; set; }
    }
}
