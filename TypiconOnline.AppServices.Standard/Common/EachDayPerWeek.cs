using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Common
{
    public class EachDayPerWeek
    {
        /// <summary>
        /// Указанное действие выполняется для каждого дня недели
        /// </summary>
        /// <param name="year"></param>
        /// <param name="action"></param>
        public static void Perform(DateTime date, Action<DateTime> action)
        {
            DateTime indexDate = GetMonday(date);

            int i = 0;

            while (i < 7)
            {
                action(indexDate);
                indexDate = indexDate.AddDays(1);
                i++;
            }
        }

        public static DateTime GetMonday(DateTime date)
        {
            date = date.Date;

            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }
    }
}
