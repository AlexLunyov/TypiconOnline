using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.AppServices.Common;

namespace TypiconOnline.AppServices.Implementations
{
    public class OutputForms : IOutputForms
    {
        private readonly TypiconDBContext _dbContext;
        private readonly IScheduleDayNameComposer _nameComposer;
        private readonly ITypiconSerializer _serializer;
        private readonly IJobRepository _jobs;

        public OutputForms(TypiconDBContext dbContext
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer serializer
            , IJobRepository jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _nameComposer = nameComposer ?? throw new ArgumentNullException(nameof(nameComposer));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Result<LocalizedOutputDay> Get(int typiconId, DateTime date, string language, HandlingMode handlingMode = HandlingMode.AstronomicDay)
        {
            date = date.Date;

            var scheduleDay = _dbContext.GetScheduleDay(typiconId, date, _serializer);

            if (scheduleDay.Success)
            {
                return Result.Ok(scheduleDay.Value.Localize(language));
            }

            var version = _dbContext.GetPublishedVersion(typiconId);

            if (version.Failure)
            {
                return Result.Fail<LocalizedOutputDay>(version.Error);
            }
            else
            {
                //добавляем задание на формирование выходных форм
                _jobs.Create(new CalculateOutputFormWeekJob(typiconId, version.Value.Id, date));

                return Result.Fail<LocalizedOutputDay>($"Инициировано формирование расписания. Повторите операцию позже.");
            }
        }

        public Result<LocalizedOutputWeek> GetWeek(int typiconId, DateTime date, string language)
        {
            date = EachDayPerWeek.GetMonday(date);

            var week = new LocalizedOutputWeek()
            {
                Name = _nameComposer.GetLocalizedWeekName(typiconId, date, language)
            };

            int i = 0;

            while (i < 7)
            {
                var dayResult = Get(typiconId, date, language);

                if (dayResult.Failure)
                {
                    return Result.Fail<LocalizedOutputWeek>(dayResult.Error);
                }

                week.Days.Add(dayResult.Value);
                date = date.AddDays(1);
                i++;
            }

            return Result.Ok(week);
        }
    }
}
