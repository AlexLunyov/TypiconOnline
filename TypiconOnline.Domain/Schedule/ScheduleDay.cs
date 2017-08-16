using System;
using TypiconOnline.Domain.Rendering;
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
            Schedule = new RenderContainer();
        }

        public string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public RenderContainer Schedule { get; set; }
        public int Sign { get; set; }
    }
}

