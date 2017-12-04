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
    public class TextScheduleDayViewer : IScheduleDayViewer
    {
        private StringBuilder _resultStringBuilder = new StringBuilder();

        public void Execute(ScheduleDay day)
        {
            if (day == null) throw new ArgumentNullException("ScheduleDay");
            if (day.Schedule == null) throw new ArgumentNullException("ScheduleDay.Schedule");

            _resultStringBuilder.Clear();

            foreach (WorshipRuleViewModel element in day.Schedule)
            {
                Render(element);
            }
        }

        private void Render(WorshipRuleViewModel element)
        {
            foreach (var item in element)
            {
                if (!string.IsNullOrEmpty(item.KindStringValue))
                {
                    _resultStringBuilder.Append($"{item.KindStringValue} ");
                }

                item.Paragraphs.ForEach(c => _resultStringBuilder.AppendLine(c));
            } 
        }



        //private void Render(WorshipRuleViewModel element)
        //{
        //if (element is WorshipRuleViewModel r)
        //{
        //    _resultStringBuilder.AppendFormat("{0} {1} {2}", r.Time, r.Name, r.AdditionalName);
        //    _resultStringBuilder.AppendLine();

        //    foreach (TextHolderViewModel childElement in r)
        //    {
        //        Render(childElement);
        //    }
        //}
        //else if (element is WorshipSequenceViewModel s)
        //{
        //    _resultStringBuilder.AppendFormat("[ {0} ]", s.Kind);
        //    _resultStringBuilder.AppendLine();

        //    foreach (ElementViewModel childElement in s.ChildElements)
        //    {
        //        Render(childElement);
        //    }
        //}
        //else if (element is YmnosStructureViewModel y)
        //{
        //    _resultStringBuilder.AppendFormat("[ {0}. {1} {2}]", y.Kind, y.IhosText, y.Ihos);
        //    _resultStringBuilder.AppendLine();

        //    foreach (ElementViewModel childElement in y.ChildElements)
        //    {
        //        Render(childElement);
        //    }
        //}
        //else if (element is ContainerViewModel c)
        //{
        //    foreach (ElementViewModel childElement in c.ChildElements)
        //    {
        //        Render(childElement);
        //    }
        //}
        //else if (element is YmnosGroupViewModel yg)
        //{
        //    _resultStringBuilder.AppendFormat("[ {0} {1}. ", yg.IhosText, yg.Ihos);

        //    if (!string.IsNullOrEmpty(yg.Self))
        //    {
        //        _resultStringBuilder.AppendFormat(". {0}", yg.Self);
        //    }
        //    else if (!string.IsNullOrEmpty(yg.Prosomoion))
        //    {
        //        _resultStringBuilder.AppendFormat(". {0}", yg.Prosomoion);
        //    }
        //    _resultStringBuilder.AppendLine("]");

        //    foreach (ElementViewModel childElement in yg.ChildElements)
        //    {
        //        Render(childElement);
        //    }
        //}
        //else if (element is TextHolderViewModel t)
        //{
        //    _resultStringBuilder.AppendFormat("[ {0} ]", t.Kind);
        //    _resultStringBuilder.AppendLine();

        //    foreach (string p in t.Paragraphs)
        //    {
        //        _resultStringBuilder.AppendLine(p);
        //    }
        //}
        //}

        public string GetResult()
        {
            return _resultStringBuilder.ToString();
        }
    }
}
