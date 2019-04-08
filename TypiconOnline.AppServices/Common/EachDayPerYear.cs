using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Common
{
    public class EachDayPerYear
    {
        /// <summary>
        /// Указанное действие выполняется для каждого дня указанного года
        /// </summary>
        /// <param name="year"></param>
        /// <param name="action"></param>
        public static void Perform(int year, Action<DateTime> action)
        {
            DateTime firstJanuary = new DateTime(year, 1, 1);

            DateTime indexDate = firstJanuary;

            firstJanuary = firstJanuary.AddYears(1);

            while (indexDate != firstJanuary)
            {
                action(indexDate);

                indexDate = indexDate.AddDays(1);
            }
        }
    }
}
