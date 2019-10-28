using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TriodionDayEditModel
    {
        public int Id { get; set; }
        public int DaysFromEaster { get; set; }
        public ItemTextStyled Name { get; set; }
        public ItemText ShortName { get; set; }
        public bool IsCelebrating { get; set; }
        public bool UseFullName { get; set; }
        public string Definition { get; set; }
    }
}
