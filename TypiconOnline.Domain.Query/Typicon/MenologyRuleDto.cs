using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class MenologyRuleDto: DayRuleDto
    {
        public ItemDate Date { get; set; }
        public ItemDate DateB { get; set; }
    }
}
