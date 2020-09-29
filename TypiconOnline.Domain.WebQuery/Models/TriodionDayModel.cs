using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TriodionDayGridModel : IGridModel
    {
        public int Id { get; set; }
        public int DaysFromEaster { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsCelebrating { get; set; }
    }
}
