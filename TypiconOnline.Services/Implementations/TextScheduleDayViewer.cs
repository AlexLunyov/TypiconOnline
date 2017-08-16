using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rendering;
using TypiconOnline.Domain.Schedule;

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

            foreach (RenderElement element in day.Schedule.ChildElements)
            {
                Render(element);
            }
        }

        private void Render(RenderElement element)
        {
            if (element is RenderServiceElement)
            {
                RenderServiceElement r = element as RenderServiceElement;
                _resultStringBuilder.AppendFormat("{0} {1} {2}", r.Time, r.Text, r.AdditionalName);
                _resultStringBuilder.AppendLine();

                foreach (RenderElement childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is RenderServiceSequence)
            {
                RenderServiceSequence r = element as RenderServiceSequence;
                _resultStringBuilder.AppendFormat("[ {0} ]", r.Kind);
                _resultStringBuilder.AppendLine();

                foreach (RenderElement childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is RenderYmnosStructure)
            {
                RenderYmnosStructure r = element as RenderYmnosStructure;
                _resultStringBuilder.AppendFormat("[ {0}. {1} {2}]", r.Kind, r.IhosText, r.Ihos);
                _resultStringBuilder.AppendLine();

                foreach (RenderElement childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is RenderYmnosGroup)
            {
                RenderYmnosGroup r = element as RenderYmnosGroup;
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

                foreach (RenderElement childElement in r.ChildElements)
                {
                    Render(childElement);
                }
            }
            else if (element is RenderTextHolder)
            {
                RenderTextHolder r = element as RenderTextHolder;
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
