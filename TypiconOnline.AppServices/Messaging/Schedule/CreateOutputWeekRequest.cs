using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Domain.Common;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateOutputWeekRequest
    {
        public int TypiconId { get; set; }
        public int TypiconVersionId { get; set; }

        private DateTime _date;
        /// <summary>
        /// Всегда устанавливается понедельник 00:00 от указанной даты
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = EachDayPerWeek.GetMonday(value.Date);
            }
        }
    }
}
