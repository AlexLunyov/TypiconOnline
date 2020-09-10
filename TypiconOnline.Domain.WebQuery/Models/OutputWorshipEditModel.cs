using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputWorshipEditModel
    {
        public int Id { get; set; }

        public string Time { get; set; }
        public virtual ItemTextStyled Name { get; set; }
        public virtual ItemTextStyled AdditionalName { get; set; }
    }
}
