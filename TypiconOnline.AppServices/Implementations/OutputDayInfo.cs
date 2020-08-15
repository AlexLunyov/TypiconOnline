using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Implementations
{
    public class OutputDayInfo
    {
        private /*readonly*/ List<DayWorship> _dayWorships = new List<DayWorship>();

        public OutputDayInfo(OutputDay day, IEnumerable<DayWorship> dayWorships
            , ScheduleResults scheduleResults, IEnumerable<BusinessConstraint> constraints)
        {
            Day = day ?? throw new ArgumentNullException(nameof(day));
            ScheduleResults = scheduleResults ?? throw new ArgumentNullException(nameof(scheduleResults));

            if (dayWorships != null)
            {
                _dayWorships = dayWorships.ToList();
            }

            if (constraints != null)
            {
                BrokenConstraints = constraints;
            }
        }

        public OutputDay Day { get; }
        public ScheduleResults ScheduleResults { get; }
        public IEnumerable<DayWorship> DayWorships => _dayWorships;
        public IEnumerable<BusinessConstraint> BrokenConstraints { get; } = new List<BusinessConstraint>();


        public void Merge(OutputDayInfo dayInfo, ITypiconSerializer typiconSerializer)
        {
            //NextDayFirstWorship
            if (ScheduleResults.NextDayFirstWorship.Count > 0
                && dayInfo.ScheduleResults.DayBefore.FirstOrDefault() is OutputWorshipModel dayBefore)
            {
                ScheduleResults.NextDayFirstWorship.Reverse();
                //вставляем службы в первую службу следующего дня (daybefore), начиная с последних
                foreach (var worship in ScheduleResults.NextDayFirstWorship)
                {
                    dayBefore.ChildElements.InsertRange(0, worship.ChildElements);
                }
            }

            //DayBefore
            //ScheduleResults.ThisDay.AddRange(dayInfo.ScheduleResults.DayBefore);

            Day.AddWorships(dayInfo.ScheduleResults.DayBefore, typiconSerializer);

            _dayWorships.AddRange(dayInfo.DayWorships);

            //удаляем возможные дубликаты ссылок на тексты служб
            _dayWorships = _dayWorships.Distinct().ToList();

            //TODO: BrokenConstaints??
        }
    }
}
