using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
    public class HtmlScheduleWeekViewer : IScheduleWeekViewer
    {
        private string _resultString;

        public string ResultString
        {
            get
            {
                return _resultString;
            }
        }

        public void Execute(ScheduleWeek week)
        {
            _resultString = "";

            _resultString += "<div class=\"schedule\">";

            //Название седмицы пропускаем. Считаем, что название седмицы будет в наименовании файла
            _resultString += "<h4 class=\"subtitle\">" + week.Name.ToUpper() + "</h4>";

            //теперь начинаем наполнять дни
            foreach (ScheduleDay day in week.Days)
            {
                _resultString += "<div style=\"margin - top:10px; \">";

                _resultString += "[sign cat=\"" + day.SignNumber.ToString() + "\"]<strong>";

                //если бдение или бдение с литией или воскресный день - красим в красный цвет
                if (day.SignNumber == 4 || day.SignNumber == 5 || day.SignNumber == 8)
                    _resultString += "<span style=\"color: #ff0000;\">";

                _resultString += day.Date.ToString("dd MMMM yyyy г.") + "<br/>";
                _resultString += day.Date.ToString("dddd").ToUpper() + "<br/>";
                _resultString += day.Name + "</strong>";

                if (day.SignNumber == 4 || day.SignNumber == 5 || day.SignNumber == 8)
                    _resultString += "</span>";

                _resultString += "</div>";

                _resultString += "<table border=0>";

                foreach (var service in day.Schedule.Worships)
                {
                    _resultString += "<tr><td>";

                    _resultString += service.Time.ToString() + "&nbsp;</td><td>";
                    _resultString += service.Name.Text;

                    //additionalName
                    if (service.AdditionalName != null)
                    {
                        _resultString += "<strong>" + service.AdditionalName.Text + "</strong>";
                    }
                    _resultString += "</td></tr>";
                }

                _resultString += "</table>";
            }

            _resultString += "</div>";
        }
    }
}
