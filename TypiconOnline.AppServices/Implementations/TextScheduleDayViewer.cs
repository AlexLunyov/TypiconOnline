using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.AppServices.Implementations
{
    public class TextScheduleDayViewer : IScheduleDayViewer<string>
    {
        private StringBuilder _resultStringBuilder = new StringBuilder();

        public string Execute(FilteredOutputDay day)
        {
            if (day == null) throw new ArgumentNullException("ScheduleDay");

            _resultStringBuilder.Clear();

            _resultStringBuilder.AppendLine(day.Date.ToShortDateString());

            if (day.Header != null)
            {
                _resultStringBuilder.AppendLine(day.Header.Name.Text);
                _resultStringBuilder.AppendLine($"Знак службы: {day.Header.SignName}");
            }
            

            foreach (var element in day.Worships)
            {
                Render(element);
            }

            return _resultStringBuilder.ToString();
        }

        public string Execute(FilteredOutputWorship viewModel)
        {
            throw new NotImplementedException();
        }

        private void Render(FilteredOutputWorship element)
        {
            _resultStringBuilder.AppendLine($"{element.Time} {element.Name} {element.AdditionalName}");

            //foreach (var item in element.ChildElements)
            //{
            //    if (item.KindText != null)
            //    {
            //        _resultStringBuilder.Append($"{item.KindText.Text} ");
            //    }

            //    item.Paragraphs.ForEach(c => _resultStringBuilder.AppendLine($"{c.Text} {c.Note?.Text}"));
            //} 
        }
    }
}
