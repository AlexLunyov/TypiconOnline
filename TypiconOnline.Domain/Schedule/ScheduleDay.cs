using System;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Schedule
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleDay //????
    {
        public ScheduleDay()
        {
            Schedule = new ExecContainer();
        }

        public string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public RuleContainer Schedule { get; set; }
    }
}

