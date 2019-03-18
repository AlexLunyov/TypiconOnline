using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class OutputForms : IOutputForms
    {
        private readonly TypiconDBContext _dbContext;
        private readonly IScheduleDayNameComposer _nameComposer;
        private readonly ITypiconSerializer _serializer;
        private readonly IOutputFormFactory _outputFormFactory;
        private readonly IQueue _queue;

        public OutputForms(TypiconDBContext dbContext
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer serializer
            , IOutputFormFactory outputFormFactory
            , IQueue queue)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _nameComposer = nameComposer ?? throw new ArgumentNullException(nameof(nameComposer));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _outputFormFactory = outputFormFactory ?? throw new ArgumentNullException(nameof(outputFormFactory));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        //public Result<ScheduleDay> Get(int typiconId, DateTime date)
        public Result<ScheduleDay> Get(int typiconId, DateTime date, HandlingMode handlingMode = HandlingMode.AstronomicDay)
        {
            var scheduleDay = _dbContext.GetScheduleDay(typiconId, date, _serializer);

            if (scheduleDay.Success)
            {
                return scheduleDay;
            }

            var version = _dbContext.GetPublishedVersion(typiconId);

            if (version.Failure)
            {
                return Result.Fail<ScheduleDay>(version.Error);
            }

            if (!CheckCalcModifiedYearExists(version.Value.Id, date, handlingMode))
            {
                return Result.Fail<ScheduleDay>($"Инициировано формирование переходящих праздников. Повторите операцию позже.");
            }

            var created = _outputFormFactory.Create(new OutputFormCreateRequest()
            {
                TypiconId = version.Value.TypiconId,
                TypiconVersionId = version.Value.Id,
                Date = date,
                HandlingMode = handlingMode
            });

            _dbContext.UpdateOutputForm(created.OutputForm);

            return Result.Ok(created.Day);
        }

        /// <summary>
        /// Проверяет, имеются ли ModifiedYear для даты
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool CheckCalcModifiedYearExists(int typiconVersionId, DateTime date, HandlingMode handlingMode)
        {
            bool jobSent = IsJobSent(typiconVersionId, date.Year);

            //Нужно проверить еще и следующий день, если вычисляем с HandlingMode.AstronomicDay
            if (handlingMode == HandlingMode.AstronomicDay)
            {
                int next = date.AddDays(1).Year;

                if (date.Year != next)
                {
                    jobSent = IsJobSent(typiconVersionId, next) || jobSent;
                }
            }

            return !jobSent;

            bool IsJobSent(int id, int year)
            {
                switch (_dbContext.IsCalcModifiedYearExists(id, year))
                {
                    case 0:
                        //заявка на вычисление ModifiedYear
                        _queue.Send(new CalculateModifiedYearJob(id, year));
                        return true;
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
        }

        //public async Task<Result<ScheduleWeek>> GetWeek(int typiconId, DateTime date)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
