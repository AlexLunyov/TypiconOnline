using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.Implementations
{
    class OutputDayInfo
    {
        private readonly List<DayWorship> _dayWorships = new List<DayWorship>();

        public OutputDayInfo(OutputDay day, IEnumerable<DayWorship> dayWorships, ScheduleResults scheduleResults)
        {
            Day = day ?? throw new ArgumentNullException(nameof(day));
            ScheduleResults = scheduleResults ?? throw new ArgumentNullException(nameof(scheduleResults));

            if (dayWorships != null)
            {
                _dayWorships = dayWorships.ToList();
            }
        }

        public OutputDay Day { get; }
        public ScheduleResults ScheduleResults { get; }
        public IEnumerable<DayWorship> DayWorships => _dayWorships;
        

        public void Merge(OutputDayInfo dayInfo)
        {
            //NextDayFirstWorship
            if (ScheduleResults.NextDayFirstWorship.Count > 0
                && dayInfo.ScheduleResults.DayBefore.FirstOrDefault() is OutputWorship dayBefore)
            {
                ScheduleResults.NextDayFirstWorship.Reverse();
                //вставляем службы в первую службу следующего дня, начиная с последних
                foreach (var worship in ScheduleResults.NextDayFirstWorship)
                {
                    dayBefore.ChildElements.InsertRange(0, worship.ChildElements);
                }
            }

            //DayBefore
            //ScheduleResults.ThisDay.AddRange(dayInfo.ScheduleResults.DayBefore);

            Day.Worships.AddRange(dayInfo.ScheduleResults.DayBefore);

            _dayWorships.AddRange(dayInfo.DayWorships);
        }
    }
}
