using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class ScheduleSettingsWeekDaysCommand : TypiconCommandBase
    {
        public ScheduleSettingsWeekDaysCommand(int id) : base(id) { }

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

        public IAuthorizeKey Key { get; }
    }
}
