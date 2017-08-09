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
            DayServices = new List<DayService>();
        }

        /// <summary>
        /// Список последований текстов служб для данного дня Устава
        /// </summary>
        public virtual List<DayService> DayServices { get; set; }

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

        /// <summary>
        /// Возвращает объединенную строку всех служб для определенного языка
        /// </summary>
        public string GetNameByLanguage(string language)
        {
            string result = "";
            if (DayServices.Count > 0)
            {
                foreach (DayService serv in DayServices)
                {
                    result += serv.ServiceName.GetTextByLanguage(language) + " ";
                }

                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
