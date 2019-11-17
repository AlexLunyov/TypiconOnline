using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyDayEditModel: DayBookModelBase
    {
        public DateTime? LeapDate { get; set; }        
    }
}
