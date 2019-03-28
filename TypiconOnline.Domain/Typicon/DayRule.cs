﻿using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class DayRule : TypiconRule, IDayRule
    {
        public DayRule() { }

        /// <summary>
        /// Список последований текстов служб для данного дня Устава
        /// </summary>
        public virtual ICollection<DayRuleWorship> DayRuleWorships { get; set; } = new List<DayRuleWorship>();

        public IReadOnlyList<DayWorship> DayWorships
        {
            get
            {
                var list = DayRuleWorships.ToArray();
                Array.Sort(list);

                return (from drw in list select drw.DayWorship).ToList();
            }
        }

        /// <summary>
        /// Возвращает объединенную строку всех служб для определенного языка
        /// </summary>
        public override string GetNameByLanguage(string language)
        {
            string result = "";
            if (DayWorships.Count() > 0)
            {
                foreach (DayWorship serv in DayWorships)
                {
                    result += serv.WorshipName.FirstOrDefault(language).Text + " ";
                }

                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
