using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OktoikhDayModel : IGridModel
    {
        public int Id { get; set; }
        public int Ihos { get; set; }
        public string DayOfWeek { get; set; }
    }
}
