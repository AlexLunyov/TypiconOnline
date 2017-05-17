using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleWeekRequest : GetScheduleDayRequest
    {
        
        public override DateTime Date
        {
            get
            {
                return base.Date;
            }

            set
            {
                //всегда понедельник
                base.Date = value;

                while (base.Date.DayOfWeek != DayOfWeek.Monday)
                {
                    base.Date = base.Date.AddDays(-1);
                }
            }
        }
    }
}
