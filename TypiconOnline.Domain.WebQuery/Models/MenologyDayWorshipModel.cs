using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// Используется как дочерняя коллекция для <see cref="MenologyRuleEditModel"/>
    /// </summary>
    public class MenologyDayWorshipModel
    {
        public int WorshipId { get; set; }
        public int Order { get; set; }
        public string Date { get; set; }
        public string LeapDate { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public virtual bool UseFullName { get; set; }
        public virtual bool IsCelebrating { get; set; }
    }
}
