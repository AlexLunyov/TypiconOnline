using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    public class ScheduleSettingsProjection
    {
        #region Дни Недели

        /// <summary>
        /// Понедельник
        /// </summary>
        public bool IsMonday { get; set; }
        /// <summary>
        /// Вторник
        /// </summary>
        public bool IsTuesday { get; set; }
        /// <summary>
        /// Среда
        /// </summary>
        public bool IsWednesday { get; set; }
        /// <summary>
        /// Четверг
        /// </summary>
        public bool IsThursday { get; set; }
        /// <summary>
        /// Пятница
        /// </summary>
        public bool IsFriday { get; set; }
        /// <summary>
        /// Суббота
        /// </summary>
        public bool IsSaturday { get; set; }
        /// <summary>
        /// Воскресенье
        /// </summary>
        public bool IsSunday { get; set; }

        #endregion

        public virtual List<int> Signs { get; set; } = new List<int>();

        public virtual List<int> MenologyRules { get; set; } = new List<int>();

        public virtual List<int> TriodionRules { get; set; } = new List<int>();

        public virtual List<DateTime> IncludedDates { get; set; } = new List<DateTime>();

        public virtual List<DateTime> ExcludedDates { get; set; } = new List<DateTime>();
    }
}
