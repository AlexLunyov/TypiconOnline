using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    class DayRuleService
    {
        private DayRule _dayRule;
        public DayRuleService(DayRule dayRule)
        {
            _dayRule = dayRule ?? throw new ArgumentNullException("DayRule");
        }

        //public IEnumerable<DayWorship> GetDayWorships()
        //{
        //    IEnumerable<DayWorship> dayWorships;

        //    return dayWorships;
        //}
    }
}
