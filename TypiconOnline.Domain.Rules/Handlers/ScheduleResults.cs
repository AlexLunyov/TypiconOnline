using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обобщенный результат работы <see cref="ScheduleHandler"/>
    /// </summary>
    public class ScheduleResults
    {
        public List<OutputWorshipModel> DayBefore { get; set; } = new List<OutputWorshipModel>();
        public List<OutputWorshipModel> ThisDay { get; set; } = new List<OutputWorshipModel>();
        public List<OutputWorshipModel> NextDayFirstWorship { get; set; } = new List<OutputWorshipModel>();

        //public ICollection<OutputWorship> Common
        //{
        //    get
        //    {
        //        return DayBefore.Concat(ThisDay)
        //            .Concat(NextDayFirstWorship)
        //            .ToList();
        //    }
        //}

        public void Clear()
        {
            DayBefore.Clear();
            ThisDay.Clear();
            NextDayFirstWorship.Clear();
        }
    }
}
