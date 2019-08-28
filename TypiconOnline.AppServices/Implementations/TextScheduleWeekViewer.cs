using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.Implementations
{
    public class TextScheduleWeekViewer : IScheduleWeekViewer<string>
    {
        private readonly IScheduleDayViewer<string> _dayViewer;

        public TextScheduleWeekViewer(IScheduleDayViewer<string> dayViewer)
        {
            _dayViewer = dayViewer ?? throw new ArgumentNullException(nameof(dayViewer));
        }

        public string Execute(int typiconId, LocalizedOutputWeek week)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine(week.Name.Text);

            foreach (var day in week.Days)
            {
                strBuilder.AppendLine("--------------------------");
                strBuilder.Append(_dayViewer.Execute(day));
            }

            return strBuilder.ToString();
        }
    }
}
