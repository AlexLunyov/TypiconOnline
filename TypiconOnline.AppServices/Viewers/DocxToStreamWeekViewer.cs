using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Viewers
{
    public class DocxToStreamWeekViewer : IScheduleWeekViewer<Result<DocxToStreamWeekResponse>>
    {
        const string DOCX_CONFIG = "DocxWeekViewer_Template";
        const string FILE_START = "Расписание ";
        const int DAYS_PER_PAGE = 4;

        private readonly IQueryProcessor _queryProcessor;
        private readonly IConfigurationRepository _configRepo;

        public DocxToStreamWeekViewer(IQueryProcessor queryProcessor, IConfigurationRepository configRepo)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _configRepo = configRepo ?? throw new ArgumentNullException(nameof(configRepo));
        }

        public Result<DocxToStreamWeekResponse> Execute(int typiconId, FilteredOutputWeek week)
        {
            if (week == null || week.Days.Count == 0)
            {
                throw new ArgumentNullException(nameof(week));
            }

            //находим путь к файлу docx
            var docxFilePath = _configRepo.GetConfigurationValue(DOCX_CONFIG);

            byte[] byteArray = File.ReadAllBytes(docxFilePath);

            if (byteArray.Length == 0)
            {
                Result.Fail<DocxToStreamWeekResponse>("Файл шаблона не найден. Обратитесь к администратору системы.");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);

                var weekViewer = new DocxScheduleWeekViewer(stream, DAYS_PER_PAGE);

                weekViewer.Execute(typiconId, week);

                DateTime date = week.Days[0].Date;

                var response = new DocxToStreamWeekResponse(stream.ToArray()
                    , "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    , GetFileName(typiconId, date));

                return Result.Ok(response);
            }
        }

        private string GetFileName(int typiconId, DateTime date)
        {
            var weekName = _queryProcessor.Process(new WeekNameQuery(typiconId, date, true));

            return $"{FILE_START} {date.ToString("yyyy-MM-dd")} {date.AddDays(6).ToString("yyyy-MM-dd")} {weekName}.docx";
        }
    }
}
