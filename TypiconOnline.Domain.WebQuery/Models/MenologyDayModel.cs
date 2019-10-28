using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MenologyDayModel : IGridModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string LeapDate { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsCelebrating { get; set; }
    }
}
