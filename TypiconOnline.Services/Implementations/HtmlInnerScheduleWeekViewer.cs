using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Вместо [sign] помещаем span на место знаков служб
    /// </summary>
    public class HtmlInnerScheduleWeekViewer : IScheduleWeekViewer
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

                int sign = SignMigrator.GetOldId(k => k.Value.NewID == day.Sign);

                _resultString += "<img style=\"margin-right: 10px;\" src=\"";
                
                switch (sign)
                {
                    case 2:
                        _resultString += "Signs/6.png\" alt = \"Шестеричная служба\">";
                        break;
                    case 3:
                        _resultString += "Signs/slav.png\" alt = \"Славословная служба\">";
                        break;
                    case 4:
                        _resultString += "Signs/pol.png\" alt = \"Полиелейная служба\">";
                        break;
                    case 5:
                        _resultString += "Signs/bd.png\" alt = \"Бденная служба\">";
                        break;
                    case 6:
                        _resultString += "Signs/lit.png\" alt = \"Бденная служба с литией\">";
                        break;
                    default:
                        _resultString += "Signs/0.png\">";
                        break;
                }

                _resultString += "<strong>";

                //если бдение или бдение с литией или воскресный день - красим в красный цвет
                if (sign == 5 || sign == 6 || sign == 9)
                    _resultString += "<span style=\"color: #ff0000;\">";

                CultureInfo ruRU = new CultureInfo("ru-RU");

                _resultString += day.Date.ToString("dd MMMM yyyy г.", ruRU) + "<br/>";
                _resultString += day.Date.ToString("dddd", ruRU).ToUpper() + "<br/>";
                _resultString += day.Name + "</strong>";

                if (sign == 5 || sign == 6 || sign == 9)
                    _resultString += "</span>";

                _resultString += "</div>";

                _resultString += "<table border=0>";

                foreach (WorshipRuleViewModel service in day.Schedule.ChildElements)
                {
                    _resultString += "<tr><td>";

                    _resultString += service.Time.ToString() + "&nbsp;</td><td>";
                    _resultString += service.Text;

                    //additionalName
                    if (!string.IsNullOrEmpty(service.AdditionalName))
                    {
                        _resultString += "<strong>" + service.AdditionalName + "</strong>";
                    }
                    _resultString += "</td></tr>";
                }

                _resultString += "</table>";
            }

            _resultString += "</div>";
        }
    }
}
