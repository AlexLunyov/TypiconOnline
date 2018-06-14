using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleWeekRequest : GetScheduleDayRequest
    {
        /// <summary>
        /// Дата всегда - понедельник. Если вводим другую - вычитает дни до ближайшего понедельника.
        /// </summary>
        public override DateTime Date
        {
            
            get
            {
                return base.Date.Date;
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
