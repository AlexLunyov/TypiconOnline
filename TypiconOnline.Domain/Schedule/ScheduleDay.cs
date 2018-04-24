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
    public class ScheduleDay 
    {
        public ScheduleDay()
        {
        }

        public string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public ViewModelRoot Schedule { get; set; } = new ViewModelRoot();
        /// <summary>
        /// Номер знака службы
        /// </summary>
        public int SignNumber { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        public string SignName { get; set; }
    }
}

