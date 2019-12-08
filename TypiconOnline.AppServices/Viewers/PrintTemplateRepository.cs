using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Viewers
{
    public class PrintTemplateRepository : IPrintTemplateRepository
    {
        private readonly IQueryProcessor _queryProcessor;

        private readonly Dictionary<int, IEnumerable<OpenXmlElement>> _dayTemplates = new Dictionary<int, IEnumerable<OpenXmlElement>>();

        public PrintTemplateRepository(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        }

        public IEnumerable<OpenXmlElement> GetDayTemplate(int typiconId, int number)
        {
            IEnumerable<OpenXmlElement> result;

            //если уже обращались, возвращаем сохраненный вариант
            if (_dayTemplates.ContainsKey(number))
            {
                result = _dayTemplates[number];
            }
            else
            {
                //иначе получаем новый
                var day = _queryProcessor.Process(new PrintDayTemplateQuery(typiconId, number));

                if (day != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Write(day.PrintFile, 0, day.PrintFile.Length);

                        using (var doc = WordprocessingDocument.Open(stream, false))
                        {
                            result = GetCleanTemplate(doc.MainDocumentPart.Document.Body.Elements().DeepClone());

                            _dayTemplates[number] = result;
                        }
                    }
                }
                else
                {
                    return default;
                }
            }
            
            return result.DeepClone();
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
            //ничего не осталось
        }
    }
}
