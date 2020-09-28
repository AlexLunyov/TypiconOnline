using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.AppServices.Implementations
{
    public class HtmlSimpleScheduleWeekViewer : IScheduleWeekViewer<string>
    {
        public string Execute(int typiconId, FilteredOutputWeek week)
        {
            string _resultString = "";

            _resultString += "<div class=\"schedule\">";

            //Название седмицы пропускаем. Считаем, что название седмицы будет в наименовании файла
            _resultString += "<h4 class=\"subtitle\">" + week.Name.Text.ToUpper() + "</h4>";

            //теперь начинаем наполнять дни
            foreach (var day in week.Days)
            {
                
                _resultString += "<div style=\"margin - top:10px; \">";

                //Sign
                if (day.Header?.Icon.HasValue == true)
                {
                    _resultString += "<img src=\"http://www.typicon.online/images/sign/" + day.Header.Icon + ".png\"/>";
                }
                
                //если бдение или бдение с литией или воскресный день - красим в красный цвет
                if (day.Header?.IsRed == true)
                    _resultString += "<span style=\"color: #ff0000;\">";

                _resultString += "<strong>" + day.Date.ToString("dd MMMM yyyy г.") + "<br/>";
                _resultString += day.Date.ToString("dddd").ToUpper() + "<br/>";

                if (day.Header != null)
                {
                    _resultString += day.Header.Name + "</strong>";
                }

                if (day.Header?.IsRed == true)
                    _resultString += "</span>";

                _resultString += "</div>";

                _resultString += "<table border=0>";

                foreach (var service in day.Worships)
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

            return _resultString;
        }
    }
}
