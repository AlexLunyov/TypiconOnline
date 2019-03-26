using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Rules.Serialization
{
    public class DayOfWeekParser
    {
        public static bool TryParse(string exp, out DayOfWeek? dayOfWeek)
        {
            bool result = false;
            if (exp == DefinitionsDayOfWeek.понедельник.ToString())
            {
                dayOfWeek = DayOfWeek.Monday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.вторник.ToString())
            {
                dayOfWeek = DayOfWeek.Tuesday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.среда.ToString())
            {
                dayOfWeek = DayOfWeek.Wednesday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.четверг.ToString())
            {
                dayOfWeek = DayOfWeek.Thursday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.пятница.ToString())
            {
                dayOfWeek = DayOfWeek.Friday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.суббота.ToString())
            {
                dayOfWeek = DayOfWeek.Saturday;
                result = true;
            }
            else if (exp == DefinitionsDayOfWeek.воскресенье.ToString())
            {
                dayOfWeek = DayOfWeek.Sunday;
                result = true;
            }
            else
            {
                dayOfWeek = null;
            }

            return result;
        }
    }

    public enum DefinitionsDayOfWeek { понедельник = 1, вторник = 2, среда = 3, четверг = 4, пятница = 5, суббота = 6, воскресенье = 7 };
}
