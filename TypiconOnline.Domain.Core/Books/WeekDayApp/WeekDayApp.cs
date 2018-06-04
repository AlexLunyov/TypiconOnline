using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Core.Books.WeekDayApp
{
    /// <summary>
    /// Приложение для текстов на каждый день недели.
    /// Содержит:
    /// - Прокимен на вечерни
	/// - Дневной тропарь(и),
	/// - Дневной кондак, 
	/// - Эксапостиларий на Утрени,
	/// - Прокимен, алиллуиа, причастен на Литургии
    /// </summary>
    public class WeekDayApp : DayStructureBase
    {
        public DayOfWeek DayOfWeek { get; set; }
    }
}
