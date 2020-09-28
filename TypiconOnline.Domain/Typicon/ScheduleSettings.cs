using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Настройки дней, когда совершаются богослужения Устава
    /// </summary>
    public class ScheduleSettings : IHasId<int>
    {
        public int Id { get; set; }

        public int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

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

        /// <summary>
        /// Знаки службы
        /// </summary>
        public virtual List<ModRuleEntitySchedule<Sign>> Signs { get; set; } = new List<ModRuleEntitySchedule<Sign>>();

        public virtual List<ModRuleEntitySchedule<MenologyRule>> MenologyRules { get; set; } = new List<ModRuleEntitySchedule<MenologyRule>>();

        public virtual List<ModRuleEntitySchedule<TriodionRule>> TriodionRules { get; set; } = new List<ModRuleEntitySchedule<TriodionRule>>();

        public virtual List<DateTime> IncludedDates { get; set; } = new List<DateTime>();

        public virtual List<DateTime> ExcludedDates { get; set; } = new List<DateTime>();

        public bool IsEveryday =>
            IsMonday
            && IsTuesday
            && IsWednesday
            && IsThursday
            && IsFriday
            && IsSaturday
            && IsSunday;
    }
}
