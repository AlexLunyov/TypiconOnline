using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OktoikhDayEditModel: BookModelBase
    {
        public int Ihos { get; set; }
        public string DayOfWeek { get; set; }
    }
}
