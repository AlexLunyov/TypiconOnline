using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputDayEditModel
    {
        public int Id { get; set; }
        public int TypiconId { get; set; }
        public ItemTextStyled Name { get; set; }

        public int PrintTemplateId { get; set; }
    }
}
