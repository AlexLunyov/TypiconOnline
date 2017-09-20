using System;
using TypiconOnline.Domain.ViewModels;
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
            Schedule = new ContainerViewModel();
        }

        public string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public ContainerViewModel Schedule { get; set; }
        /// <summary>
        /// Номер знака службы
        /// </summary>
        public int Sign { get; set; }
    }
}

