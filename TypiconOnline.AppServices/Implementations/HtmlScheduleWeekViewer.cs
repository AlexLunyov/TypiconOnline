using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Implementations
{
    public class HtmlScheduleWeekViewer : IScheduleWeekViewer<string>
    {
        const string XSLT_CONFIG = "ScheduleWeekViewer_XsltFile";
        readonly ITypiconSerializer xmlSerializer;
        readonly IConfigurationRepository configRepo;

        public HtmlScheduleWeekViewer(ITypiconSerializer xmlSerializer, IConfigurationRepository configRepo)
        {
            this.xmlSerializer = xmlSerializer ?? throw new ArgumentNullException("xmlSerializer in HtmlScheduleDayViewer");
            this.configRepo = configRepo ?? throw new ArgumentNullException("configRepo in HtmlScheduleDayViewer");
        }

        public string Execute(int typiconId, FilteredOutputWeek week)
        {
            //находим путь к файлу xslt
            var xsltFilePath = configRepo.GetConfigurationValue(XSLT_CONFIG);

            var xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath);

            //получаем xml-строку
            var xml = xmlSerializer.Serialize(week);

            var xmlreader = XmlReader.Create(new StringReader(xml));

            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter);

            //выполняем xslt-трансформацию
            xslt.Transform(xmlreader, xmlWriter);

            return stringWriter.ToString();
        }

        //public string Execute(int typiconId, FilteredOutputWeek week)
        //{
        //    string _resultString = "";

        //    _resultString += "<div class=\"schedule\">";

        //    //Название седмицы пропускаем. Считаем, что название седмицы будет в наименовании файла
        //    _resultString += "<h4 class=\"subtitle\">" + week.Name.Text.ToUpper() + "</h4>";

        //    //теперь начинаем наполнять дни
        //    foreach (var day in week.Days)
        //    {
        //        _resultString += "<div style=\"margin - top:10px; \">";

        //        _resultString += "[sign cat=\"" + day.SignNumber.ToString() + "\"]<strong>";

        //        //если бдение или бдение с литией или воскресный день - красим в красный цвет
        //        if (day.SignNumber == 4 || day.SignNumber == 5 || day.SignNumber == 8)
        //            _resultString += "<span style=\"color: #ff0000;\">";

        //        _resultString += day.Date.ToString("dd MMMM yyyy г.") + "<br/>";
        //        _resultString += day.Date.ToString("dddd").ToUpper() + "<br/>";
        //        _resultString += day.Name.Text + "</strong>";

        //        if (day.SignNumber == 4 || day.SignNumber == 5 || day.SignNumber == 8)
        //            _resultString += "</span>";

        //        _resultString += "</div>";

        //        _resultString += "<table border=0>";

        //        foreach (var service in day.Worships)
        //        {
        //            _resultString += "<tr><td>";

        //            _resultString += service.Time.ToString() + "&nbsp;</td><td>";
        //            _resultString += service.Name.Text;

        //            //additionalName
        //            if (service.AdditionalName != null)
        //            {
        //                _resultString += "<strong>" + service.AdditionalName.Text + "</strong>";
        //            }
        //            _resultString += "</td></tr>";
        //        }

        //        _resultString += "</table>";
        //    }

        //    _resultString += "</div>";

        //    return _resultString;
        //}
    }
}
