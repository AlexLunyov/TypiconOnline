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
            DayRuleWorships = new List<DayRuleWorships>();
        }

        /// <summary>
        /// Список последований текстов служб для данного дня Устава
        /// </summary>
        public virtual List<DayRuleWorships> DayRuleWorships { get; set; }

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

        private List<DayWorship> _dayWorships;

        public List<DayWorship> DayWorships
        {
            get
            {
                if (_dayWorships == null)
                {
                    _dayWorships = (from drw in DayRuleWorships select drw.DayWorship).ToList();
                }
                return _dayWorships;
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
