﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// Используется как дочерняя коллекция для <see cref="TriodionRuleCreateEditModel"/>
    /// </summary>
    public class TriodionDayWorshipModel
    {
        public int WorshipId { get; set; }
        public int Order { get; set; }
        public int DaysFromEaster { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public virtual bool UseFullName { get; set; }
        public virtual bool IsCelebrating { get; set; }
    }
}
