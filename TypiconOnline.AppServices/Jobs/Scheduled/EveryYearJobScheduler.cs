using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    /// <summary>
    /// Расписание на каждый год
    /// </summary>
    public class EveryYearJobScheduler : IJobScheduler
    {
        private ItemDate _date;
        private ItemTime _time;

        public EveryYearJobScheduler(int month, int day, int hours, int minutes)
        {
            _date = new ItemDate(month, day);

            if (!_date.IsValid)
            {
                throw new ArgumentOutOfRangeException("Неверное заполнение даты.");
            }

            _time = new ItemTime(hours, minutes);

            if (!_time.IsValid)
            {
                throw new ArgumentOutOfRangeException("Неверное заполнение времени.");
            }
        }
        public int Month => _date.Month;
        public int Day => _date.Day;
        public int Hours => _time.Hour;
        public int Minutes => _time.Minute;

        public DateTime NextDate
        {
            get
            {
                var now = DateTime.Now;
                var date = new DateTime(now.Year, Month, Day, Hours, Minutes, 0);

                if (now > date)
                {
                    date = date.AddYears(1);
                }

                return date;
            }
        }
    }
}
