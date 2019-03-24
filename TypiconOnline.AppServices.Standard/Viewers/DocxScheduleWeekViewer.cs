using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Viewers
{
    public class DocxScheduleWeekViewer : IScheduleWeekViewer, IDisposable
    {
        //string _fileName;
        int _daysPerPage;

        Stream _stream;

        /// <summary>
        /// Флаг для сохранения файла в процессе обработки ScheduleWeek
        /// </summary>
        private readonly bool _isFromFileName = false;

        public DocxScheduleWeekViewer(string fileName, int daysPerPage)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            _daysPerPage = daysPerPage;
            _isFromFileName = true;

            _stream = File.Open(fileName, FileMode.Open);
        }

        public DocxScheduleWeekViewer(Stream stream, int daysPerPage)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _daysPerPage = daysPerPage;
        }

        public void Execute(ScheduleWeek week)
        {
            if (week == null)
            {
                throw new ArgumentNullException(nameof(week));
            }

            using (WordprocessingDocument doc = WordprocessingDocument.Open(_stream, true))
            {
                //просматриваем все элементы ChildElements и выбираем только таблицы
                //считаем, что всего таблиц 10. 
                //0 - шапка с названием седмицы, а остальные - шаблоны для обозначения знаков служб

                //эта коллекция будет вместилищем таблиц - docx-шаблонов
                var templateTables = new List<Table>();
                //ищем их все и добавляем в коллекцию
                foreach (OpenXmlElement element in doc.MainDocumentPart.Document.Body.ChildElements)
                {
                    if (element.GetType() == typeof(Table))
                        templateTables.Add((Table)element);
                }

                //создаем коллекцию таблиц, которые будут результирующим содержанием выходного документа
                List<OpenXmlElement> resultElements = new List<OpenXmlElement>();

                int i = _daysPerPage;

                bool firstTime = true;

                foreach (ScheduleDay day in week.Days)
                {
                    //шапка
                    if (i == _daysPerPage)
                    {
                        if (!firstTime)
                        {
                            //вставляем разрыв страницы
                            resultElements.Add(GetPageBreak());
                        }
                        firstTime = false;

                        //Название седмицы
                        //table[2]->tr[1]->td[1]->p[1]->r[1]->t[1]
                        Table headerTable = GetHeaderTable(templateTables[0], week.Name.Text);
                        resultElements.Add(headerTable);
                    }
                    Table dayTable = GetDayTable(day, templateTables);
                    //добавляем таблицу к выходной коллекции
                    resultElements.Add(dayTable);

                    i--;
                    if (i == 0)
                    {
                        i = _daysPerPage;
                    }
                }

                //в конце удаляем все из документа, оставляя только колонтитулы (прописаны в SectionProperties) и результирующие таблицы

                foreach (OpenXmlElement el in doc.MainDocumentPart.Document.Body.ChildElements)
                {
                    if (el.GetType() == typeof(SectionProperties))
                    {
                        resultElements.Add(el);
                        break;
                    }
                }

                doc.MainDocumentPart.Document.Body.RemoveAllChildren();
                foreach (OpenXmlElement t in resultElements)
                {
                    doc.MainDocumentPart.Document.Body.AppendChild(t);
                }

                if (_isFromFileName)
                {
                    //если место было указано из файла, сохраняем его
                    doc.MainDocumentPart.Document.Save();
                }
            }
        }

        private Paragraph GetPageBreak()
        {
            Paragraph paragraph232 = new Paragraph(
              new Run(
                new Break() { Type = BreakValues.Page }));

            return paragraph232;
        }

        private Table GetHeaderTable(Table sourceTable, string weekName)
        {
            var header = sourceTable.CloneNode(true);

            TableCell tdWeekName = (TableCell)header.ChildElements[2].ChildElements[1];
            SetTextToCell(tdWeekName, weekName, false, false);

            return (Table) header;
        }

        private Table GetDayTable(ScheduleDay day, List<Table> templateTables)
        {
            Table dayTable = new Table();

            try
            {
                //в зависимости от того, какой знак дня - берем для заполнения шаблона соответствующую таблицу в templateTables
                Table dayTemplateTable = templateTables[day.SignNumber + 1] ?? templateTables[1];

                TableRow tr = (TableRow)dayTemplateTable.ChildElements[2].Clone();
                TableCell tdDayofweek = (TableCell)tr.ChildElements[2];
                TableCell tdName = (TableCell)tr.ChildElements[3];
                string sDayofweek = day.Date.ToString("dddd").ToUpper();
                string sName = day.Name.Text;
                //TODO: реализовать функционал
                bool bIsNameBold = false;//(dayNode.SelectSingleNode("name").Attributes["isbold"] != null);

                SetTextToCell(tdDayofweek, sDayofweek, false, false);
                SetTextToCell(tdName, sName, bIsNameBold, false);
                dayTable.AppendChild(tr);

                tr = (TableRow)dayTemplateTable.ChildElements[3].Clone();
                TableCell tdDate = (TableCell)tr.ChildElements[2];
                string sDate = day.Date.ToString("dd MMMM yyyy г.");
                SetTextToCell(tdDate, sDate, false, false);
                dayTable.AppendChild(tr);

                foreach (WorshipRuleViewModel service in day.Worships)
                {
                    tr = (TableRow)dayTemplateTable.ChildElements[4].Clone();
                    TableCell tdTime = (TableCell)tr.ChildElements[2];
                    TableCell tdSName = (TableCell)tr.ChildElements[3];

                    string sTime = service.Time.ToString();
                    string sSName = service.Name.Text;

                    bool bIsTimeBold = false; //(serviceNode.Attributes["istimebold"] != null);
                    bool bIsTimeRed = false; //(serviceNode.Attributes["istimered"] != null);

                    bool bIsServiceNameBold = false; //(serviceNode.Attributes["isnamebold"] != null);
                    bool bIsServiceNameRed = false; //(serviceNode.Attributes["isnamered"] != null);


                    SetTextToCell(tdTime, sTime, bIsTimeBold, bIsTimeRed);
                    SetTextToCell(tdSName, sSName, bIsServiceNameBold, bIsServiceNameRed);

                    //additionalName
                    if (service.AdditionalName != null)
                    {
                        AppendTextToCell(tdSName, service.AdditionalName.Text, true, false);
                    }

                    //tr.ChildElements[1].InnerXml = dayNode.SelectSingleNode("time").InnerText;
                    //tr.ChildElements[2].InnerXml      = dayNode.SelectSingleNode("name").InnerText;
                    dayTable.AppendChild(tr);
                }
            }
            catch (IndexOutOfRangeException) { }

            return dayTable;
        }

        /// <summary>
        /// Функция задает строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        private void SetTextToCell(TableCell td, string text, bool isBold, bool isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph)td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }
            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }

            Text t = (Text)r.ChildElements[1];
            t.Text = text;

            p.RemoveAllChildren<Run>();
            p.Append(r);
        }

        /// <summary>
        /// Функция добавляет строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        /// <param name="isBold"></param>
        /// <param name="isRed"></param>
        private void AppendTextToCell(TableCell td, string text, Boolean isBold, Boolean isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph)td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }

            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }
            else
                properties.Bold = null;

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }
            else
            {
                properties.Color = null;
            }

            Text t = (Text)r.ChildElements[1];
            t.Text = " " + text;
            //настройка, чтобы не резал пробелы
            t.Space = SpaceProcessingModeValues.Preserve;

            p.Append(r);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
