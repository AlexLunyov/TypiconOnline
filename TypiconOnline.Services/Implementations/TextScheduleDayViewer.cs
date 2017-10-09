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
            if (day == null)
            {
                throw new ArgumentNullException("ScheduleDay");
            }
            if (day.Schedule == null)
            {
                throw new ArgumentNullException("ScheduleDay.Schedule");
            }

            _resultStringBuilder.Clear();

            foreach (ElementViewModel element in day.Schedule.ChildElements)
            {
                Render(element);
            }
        }

        private void Render(ElementViewModel element)
        {
            if (element is ServiceViewModel)
            {
                ServiceViewModel r = element as ServiceViewModel;
                _resultStringBuilder.AppendFormat("{0} {1} {2}", r.Time, r.Text, r.AdditionalName);
                _resultStringBuilder.AppendLine();

                foreach (ElementViewModel childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is ServiceSequenceViewModel)
            {
                ServiceSequenceViewModel r = element as ServiceSequenceViewModel;
                _resultStringBuilder.AppendFormat("[ {0} ]", r.Kind);
                _resultStringBuilder.AppendLine();

                foreach (ElementViewModel childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is YmnosStructureViewModel)
            {
                YmnosStructureViewModel r = element as YmnosStructureViewModel;
                _resultStringBuilder.AppendFormat("[ {0}. {1} {2}]", r.Kind, r.IhosText, r.Ihos);
                _resultStringBuilder.AppendLine();

                foreach (ElementViewModel childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is KekragariaRuleViewModel)
            {
                KekragariaRuleViewModel r = element as KekragariaRuleViewModel;

                foreach (ElementViewModel childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is YmnosGroupViewModel)
            {
                YmnosGroupViewModel r = element as YmnosGroupViewModel;
                _resultStringBuilder.AppendFormat("[ {0} {1}. ", r.IhosText, r.Ihos);

                if (!string.IsNullOrEmpty(r.Self))
                {
                    _resultStringBuilder.AppendFormat(". {0}", r.Self);
                }
                else if (!string.IsNullOrEmpty(r.Prosomoion))
                {
                    _resultStringBuilder.AppendFormat(". {0}", r.Prosomoion);
                }
                _resultStringBuilder.AppendLine("]");

                foreach (ElementViewModel childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is TextHolderViewModel)
            {
                TextHolderViewModel r = element as TextHolderViewModel;
                _resultStringBuilder.AppendFormat("[ {0} ]", r.Kind);
                _resultStringBuilder.AppendLine();

                foreach (string p in r.Paragraphs)
                {
                    _resultStringBuilder.AppendLine(p);
                }
            }
        }

        public string GetResult()
        {
            return _resultStringBuilder.ToString();
        }
    }
}
