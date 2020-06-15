﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Viewers
{
    public class PrintTemplateRepository : IPrintTemplateRepository, IDisposable
    {
        private readonly IQueryProcessor _queryProcessor;

        private readonly Dictionary<int, (WordprocessingDocument doc, MemoryStream stream)> _docs = new Dictionary<int, (WordprocessingDocument, MemoryStream)>();

        private readonly Dictionary<int, GetDayTemplateResponse> _dayTemplates = new Dictionary<int, GetDayTemplateResponse>();

        public PrintTemplateRepository(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        }

        public GetDayTemplateResponse GetDayTemplate(int typiconId, int number)
        {
            WordprocessingDocument doc;

            //если уже обращались, возвращаем сохраненный вариант
            if (_dayTemplates.ContainsKey(number))
            {
                doc = _docs[number].doc;
            }
            else
            {
                //иначе получаем новый
                var day = _queryProcessor.Process(new PrintDayTemplateQuery(typiconId, number));

                if (day != null)
                {
                    var stream = new MemoryStream();

                    stream.Write(day.PrintFile, 0, day.PrintFile.Length);

                    doc = WordprocessingDocument.Open(stream, false);

                    _docs[number] = (doc, stream);
                }
                else
                {
                    return default;
                }
            }

            var xmlElements = GetCleanTemplate(doc.MainDocumentPart.Document.Body.Elements().DeepClone());

            return new GetDayTemplateResponse(xmlElements, doc.MainDocumentPart);
        }

        /// <summary>
        /// Удаляет пустой параграф, если определена в документе только таблица
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        private IEnumerable<OpenXmlElement> GetCleanTemplate(IEnumerable<OpenXmlElement> elements)
        {
            var list = elements.ToList();

            if (list.Count == 3
                && list.OfType<Table>().Count() == 1
                && list.OfType<Paragraph>().FirstOrDefault() is Paragraph p
                && string.IsNullOrEmpty(p.InnerText))
            {
                list.Remove(p);
            }

            return list;
        }

        /// <summary>
        /// Освобождаем память
        /// </summary>
        public void Dispose()
        {
            foreach (var element in _docs)
            {
                element.Value.doc.Dispose();
                element.Value.stream.Dispose();
            }
        }
    }
}
