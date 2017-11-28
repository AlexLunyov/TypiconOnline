using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class DayRule : TypiconRule
    {
        public DayRule()
        {
            DayRuleWorships = new List<DayRuleWorship>();
        }

        /// <summary>
        /// Список последований текстов служб для данного дня Устава
        /// </summary>
        public virtual List<DayRuleWorship> DayRuleWorships { get; set; }

        /// <summary>
        /// Возвращает объединенную строку всех служб для языка по умолчанию для данного Устава
        /// </summary>
        public override string Name
        {
            get
            {
                return GetNameByLanguage((Owner != null) ? Owner.Settings.DefaultLanguage : "");
            }
        }

        public List<DayWorship> DayWorships
        {
            get
            {
                return (from drw in DayRuleWorships select drw.DayWorship).ToList();
            }
        }

        /// <summary>
        /// Возвращает объединенную строку всех служб для определенного языка
        /// </summary>
        public string GetNameByLanguage(string language)
        {
            string result = "";
            if (DayWorships.Count > 0)
            {
                foreach (DayWorship serv in DayWorships)
                {
                    result += serv.WorshipName[language] + " ";
                }

                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
