using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class ScheduleSettingsWeekDaysModel
    {
        public int TypiconId { get; set; }

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

        //public virtual List<SignScheduleModel> Signs { get; set; } = new List<SignScheduleModel>();

        //public virtual List<MenologyRuleScheduleModel> MenologyRules { get; set; } = new List<MenologyRuleScheduleModel>();

        //public virtual List<TriodionRuleScheduleModel> TriodionRules { get; set; } = new List<TriodionRuleScheduleModel>();

        public virtual List<DateTime> IncludedDates { get; set; } = new List<DateTime>();

        public virtual List<DateTime> ExcludedDates { get; set; } = new List<DateTime>();
    }

    public class DateGridItem: IGridModel
    {
        public DateTime Date { get; set; }
    }

    //public class SignScheduleModel : IGridModel
    //{
    //    public int RuleId { get; set; }
    //    public string Name { get; set; }
    //    public int? PrintTemplateId { get; set; }
    //}
}
