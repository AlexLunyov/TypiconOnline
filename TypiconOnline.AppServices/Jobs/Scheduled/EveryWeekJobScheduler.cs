using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    /// <summary>
    /// Возвращает следующую дату на каждую неделю
    /// </summary>
    public class EveryWeekJobScheduler : IJobScheduler
    {
        private ItemTime _time;

        public EveryWeekJobScheduler(DayOfWeek dayOfWeek, int hours, int minutes)
        {
            if (hours < 0 || hours > 23)
            {
                throw new IndexOutOfRangeException(nameof(hours));
            }

            if (minutes < 0 || minutes > 59)
            {
                throw new IndexOutOfRangeException(nameof(minutes));
            }

            _time = new ItemTime(hours, minutes);

            if (!_time.IsValid)
            {
                throw new ArgumentOutOfRangeException("Неверное заполнение времени.");
            }

            DayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek { get; }
        public int Hours => _time.Hour;
        public int Minutes => _time.Minute;

        public DateTime NextDate
        {
            get
            {
                var date = DateTime.Now;

                //если тот же день недели, переносим ровно на неделю
                if (date.DayOfWeek == DayOfWeek)
                {
                    date = date.AddDays(7);
                }
                else
                {
                    //иначе находим следующие день недели
                    while (date.DayOfWeek != DayOfWeek)
                    {
                        date = date.AddDays(1);
                    }
                }
                

                return new DateTime(date.Year, date.Month, date.Day, Hours, Minutes, 0);
            }
        }
    }
}
