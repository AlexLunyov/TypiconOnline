using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Задача для вычисления Сформированных расписаний при изменении Устава
    /// </summary>
    public class RecalculateCompiledSchedulesJob : JobBase
    {
        public int TypiconId { get; set; }
        //public int UserId { get; set; }
    }
}
