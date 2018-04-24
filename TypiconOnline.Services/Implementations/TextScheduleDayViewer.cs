using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
    public class TextScheduleDayViewer : IScheduleDayViewer<string>
    {
        private StringBuilder _resultStringBuilder = new StringBuilder();

        public string Execute(ScheduleDay day)
        {
            if (day == null) throw new ArgumentNullException("ScheduleDay");
            if (day.Schedule == null) throw new ArgumentNullException("ScheduleDay.Schedule");

            _resultStringBuilder.Clear();

            foreach (WorshipRuleViewModel element in day.Schedule.Worships)
            {
                Render(element);
            }

            return _resultStringBuilder.ToString();
        }

        private void Render(WorshipRuleViewModel element)
        {
            foreach (var item in element.ChildElements)
            {
                if (!string.IsNullOrEmpty(item.KindStringValue))
                {
                    _resultStringBuilder.Append($"{item.KindStringValue} ");
                }

                item.Paragraphs.ForEach(c => _resultStringBuilder.AppendLine($"{c.Text} {c.Note?.Text}"));
            } 
        }
    }
}
