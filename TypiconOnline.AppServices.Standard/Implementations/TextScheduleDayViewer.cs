using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
    public class TextScheduleDayViewer : IScheduleDayViewer<string>
    {
        private StringBuilder _resultStringBuilder = new StringBuilder();

        public string Execute(ScheduleDay day)
        {
            if (day == null) throw new ArgumentNullException("ScheduleDay");

            _resultStringBuilder.Clear();

            foreach (WorshipRuleViewModel element in day.Worships)
            {
                Render(element);
            }

            return _resultStringBuilder.ToString();
        }

        public string Execute(WorshipRuleViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private void Render(WorshipRuleViewModel element)
        {
            foreach (var item in element.ChildElements)
            {
                if (!string.IsNullOrEmpty(item.KindStringValue))
                {
                    _resultStringBuilder.Append($"{item.KindStringValue} ");
                }

                item.Paragraphs.ForEach(c => _resultStringBuilder.AppendLine($"{c.Text.Text} {c.Note?.Text.Text}"));
            } 
        }
    }
}
