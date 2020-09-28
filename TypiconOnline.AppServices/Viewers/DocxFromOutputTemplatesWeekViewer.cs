using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Viewers
{
    public class DocxFromOutputTemplatesWeekViewer : IScheduleWeekViewer<Result<FileDownloadResponse>>
    {
        const string FILE_START = "Расписание ";

        private readonly IQueryProcessor _queryProcessor;

        public DocxFromOutputTemplatesWeekViewer(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        }

        public Result<FileDownloadResponse> Execute(int typiconId, FilteredOutputWeek week)
        {
            if (week == null || week.Days.Count == 0)
            {
                return Result.Fail<FileDownloadResponse>("Ошибка: выходная форма седмицы не имеет дней богослужения.");
            }

            var result = PerformWork(typiconId, week);

            if (!result.Success)
            {
                return Result.Fail<FileDownloadResponse>(result.Error);
            }

            DateTime date = week.Days[0].Date;

            var response = new FileDownloadResponse(result.Value
                , "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                , GetFileName(typiconId, date));

            return Result.Ok(response);
        }

        private Result<byte[]> PerformWork(int typiconId, FilteredOutputWeek week)
        {
            var weekTemplate = _queryProcessor.Process(new PrintWeekTemplateQuery(typiconId));

            if (weekTemplate == null)
            {
                return Result.Fail<byte[]>($"Печатный шаблон седмицы для Устава с Id={typiconId} не был найден.");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(weekTemplate.PrintFile, 0, weekTemplate.PrintFile.Length);

                var errors = new StringBuilder();

                using (WordprocessingDocument weekDoc = WordprocessingDocument.Open(stream, true))
                {
                    SetWeekName(weekDoc, week);

                    var placeToPasteSchedule = weekDoc.MainDocumentPart.Document.Body
                    .FindElementsByText(OutputTemplateConstants.DaysPlacement)
                    .FirstOrDefault();

                    if (placeToPasteSchedule == null)
                    {
                        return Result.Fail<byte[]>("Ошибка: в шаблоне седмицы не определено место для вставки дней.");
                    }
                    //находим элемент, после которого можно вставлять шаблоны дней
                    var parentToPasteSchedule = GetParentToPasteSchedule(placeToPasteSchedule);
                    /*var */parentToPasteSchedule = weekDoc.MainDocumentPart.Document.Body.LastChild;

                    int d = week.Days.Count % weekTemplate.DaysPerPage;

                    using (var dayRep = new PrintTemplateRepository(_queryProcessor))
                    {
                        for (int i = week.Days.Count - 1; i >= 0; i--)
                        {
                            var day = week.Days[i];

                            var modDayTemplate = GetFilledDayElements(dayRep, typiconId, day);

                            if (modDayTemplate.Success)
                            {
                                //добавляем в конечный документ ссылочные объекты (изображения)
                                CopyRelativeElements(weekDoc, modDayTemplate.Value.DocumentPart, modDayTemplate.Value.XmlElements);

                                foreach (var ins in modDayTemplate.Value.XmlElements)
                                {
                                    parentToPasteSchedule.InsertAfterSelf(ins);
                                    //ins.CopyRelativeElements(weekDoc, modDayTemplate.Value.DocumentPart);
                                }

                                d--;
                                if (d == 0 && i > 0)
                                {
                                    //вставляем разрыв страницы
                                    parentToPasteSchedule.InsertAfterSelf(PageBreak);
                                    d = weekTemplate.DaysPerPage;
                                }
                            }
                            else
                            {
                                errors.AppendLine(modDayTemplate.Error);
                            }
                        }
                    }

                    RemovePlaceToPaste(placeToPasteSchedule);

                    weekDoc.MainDocumentPart.Document.Save();

                    weekDoc.Close();
                }

                return (errors.Length == 0)
                        ? Result.Ok(stream.ToArray())
                        : Result.Fail<byte[]>(errors.ToString());
            }
        }

        /// <summary>
        /// Если параграф содержит только место замены - удаляем и его
        /// </summary>
        /// <param name="placeToPasteSchedule"></param>
        private void RemovePlaceToPaste(OpenXmlElement placeToPasteSchedule)
        {
            if (placeToPasteSchedule.Parent is Paragraph p && p.InnerText == OutputTemplateConstants.DaysPlacement)
            {
                placeToPasteSchedule = placeToPasteSchedule.Parent;
            }

            placeToPasteSchedule.Parent.RemoveChild(placeToPasteSchedule);
        }

        /// <summary>
        /// Возвращает предкорневой элемент документа
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private OpenXmlElement GetParentToPasteSchedule(OpenXmlElement element)
        {
            return (element.Parent is Body) ? element : GetParentToPasteSchedule(element.Parent);
        }

        /// <summary>
        /// Копирует в <paramref name="weekDoc"/> объекты, на которые ссылается шаблон дня
        /// </summary>
        /// <param name="weekDoc"></param>
        /// <param name="modDayTemplate"></param>
        private void CopyRelativeElements(WordprocessingDocument weekDoc, MainDocumentPart documentPart, IEnumerable<OpenXmlElement> elements)
        {
            foreach (var oldPart in documentPart.ImageParts)
            {
                var newPart = weekDoc.MainDocumentPart.AddImagePart(oldPart.ContentType);
                newPart.FeedData(oldPart.GetStream(FileMode.Open, FileAccess.Read));

                ReplaceRelationShips(elements, documentPart.GetIdOfPart(oldPart), weekDoc.MainDocumentPart.GetIdOfPart(newPart));
            }
        }

        private void ReplaceRelationShips(IEnumerable<OpenXmlElement> elements, string oldId, string newId)
        {
            foreach (var element in elements)
            {
                element.Descendants<DocumentFormat.OpenXml.Drawing.Blip>()
                    .Where(c => c.Embed == oldId)
                    .ToList()
                    .ForEach(blip =>
                    {
                        blip.Embed = newId;
                    });
            }
        }

        /// <summary>
        /// Вставляет наименование седмицы в шаблон
        /// </summary>
        /// <param name="weekDoc"></param>
        /// <param name="week"></param>
        private void SetWeekName(WordprocessingDocument weekDoc, FilteredOutputWeek week)
        {
            //находим место для вставки

            //документ
            List<Run> places = weekDoc.MainDocumentPart.Document.Body
                .FindElementsByText(OutputTemplateConstants.WeekName)
                .ToList();

            //заголовки
            foreach (var h in weekDoc.MainDocumentPart.HeaderParts)
            {
                places.AddRange(h.Header.FindElementsByText(OutputTemplateConstants.WeekName));
            }

            //footnotes
            foreach (var h in weekDoc.MainDocumentPart.FooterParts)
            {
                places.AddRange(h.Footer.FindElementsByText(OutputTemplateConstants.WeekName));
            }

            //заменям значения
            places.ForEach(c => c.ReplaceText(OutputTemplateConstants.WeekName, week.Name.Text));
        }

        /// <summary>
        /// Возвращает заполненный шаблон дня и список используемых картинок для вставки в итоговый документ
        /// </summary>
        /// <param name="dayRep"></param>
        /// <param name="typiconId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private Result<GetDayTemplateResponse> GetFilledDayElements(PrintTemplateRepository dayRep, int typiconId, FilteredOutputDay day)
        {
            var dayTemplate = dayRep.GetDayTemplate(typiconId, day.Header?.SignNumber);

            if (dayTemplate == null)
            {
                return Result.Fail<GetDayTemplateResponse>($"Ошибка: печатный шаблон дня не был найден");
            }

            //заменяем дату - обязательно
            var dateReplacement = dayTemplate.XmlElements.ReplaceElementsByText(OutputTemplateConstants.Date, day.Date.ToString("dd MMMM yyyy г."));

            //заменяем день недели - не обязательно
            var dayOfWeekReplacement = dayTemplate.XmlElements.ReplaceElementsByText(OutputTemplateConstants.DayOfWeek, day.Date.ToString("dddd"));

            //заменяем имя дня - не обязательно
            string dayName = (day.Header != null) ? day.Header.Name.Text : string.Empty;
            var dayNameReplacement = dayTemplate.XmlElements.ReplaceElementsByText(OutputTemplateConstants.DayName, dayName);

            //вставляем службы
            var worshipsAppending = AppendWorships(dayTemplate.XmlElements, day.Worships);

            var res = Result.Combine(dateReplacement
                                    //, dayOfWeekReplacement
                                    //, dayNameReplacement
                                    , worshipsAppending);

            return (res.Success) 
                ? Result.Ok(dayTemplate) 
                : Result.Fail<GetDayTemplateResponse>(res.Error);
        }

        /// <summary>
        /// Добавляет коллекцию служб в печатный шаблон дня
        /// </summary>
        /// <param name="templateElements"></param>
        /// <param name="worships"></param>
        /// <returns></returns>
        private Result AppendWorships(IEnumerable<OpenXmlElement> templateElements, List<FilteredOutputWorship> worships)
        {
            var timePlacement = templateElements
                .FindElementsByText(OutputTemplateConstants.Time)
                .FirstOrDefault();

            var worshipNamePlacement = templateElements
                .FindElementsByText(OutputTemplateConstants.WorshipName)
                .FirstOrDefault();

            if (timePlacement == null )
            {
                return Result.Fail($"Поле {OutputTemplateConstants.Time} должно быть определено внутри шаблона");
            }

            if (worshipNamePlacement == null)
            {
                return Result.Fail($"Поле {OutputTemplateConstants.WorshipName} должно быть определено внутри шаблона");
            }

            OpenXmlElement row = DocxUtility.FindCommonParent<TableRow>(timePlacement, worshipNamePlacement);

            if (row == null)
            {
                row = DocxUtility.FindCommonParent<Paragraph>(timePlacement, worshipNamePlacement);
            }

            if (row == null)
            {
                return Result.Fail($"Поля {OutputTemplateConstants.Time} и {OutputTemplateConstants.WorshipName} должны находиться либо внутри одной строки таблицы либо в одном параграфе документа");
            }

            foreach (var w in worships)
            {
                var r = row.CloneNode(true);

                //time
                r.ReplaceElementsByText(OutputTemplateConstants.Time, w.Time);
                //worship
                r.ReplaceElementsByWorship(OutputTemplateConstants.WorshipName, w);

                row.InsertBeforeSelf(r);
            }

            row.Parent.RemoveChild(row);

            return Result.Ok();
        }

        private string GetFileName(int typiconId, DateTime date)
        {
            var weekName = _queryProcessor.Process(new WeekNameQuery(typiconId, date, true));

            return $"{FILE_START} {date.ToString("yyyy-MM-dd")} {date.AddDays(6).ToString("yyyy-MM-dd")} {weekName}.docx";
        }

        private Paragraph PageBreak
            => new Paragraph(
                  new Run(
                    new Break() { Type = BreakValues.Page }));
    }
}
