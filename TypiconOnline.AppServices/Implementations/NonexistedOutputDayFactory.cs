using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Фабрика по вычислению выходных форм.
    /// Вычисляет только несуществующие в бд дни
    /// </summary>
    public class NonexistedOutputDayFactory : OutputDayFactory
    {
        private readonly TypiconDBContext _dbContext;

        public NonexistedOutputDayFactory(TypiconDBContext dbContext
            , IScheduleDataCalculator dataCalculator
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer typiconSerializer
            , ScheduleHandler handler) : base(dataCalculator, nameComposer, typiconSerializer, handler)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override CreateOutputDayResponse InnerCreate(CreateOutputDayRequest req, ref OutputDayInfo dayInfo, IScheduleDataCalculator dataCalculator)
        {
            var exists =_dbContext.IsOutputDayExists(req.TypiconId, req.Date);

            if (!exists)
            {
                return base.InnerCreate(req, ref dayInfo, dataCalculator);
            }
            else
            {
                dayInfo = null;

                return default;
            }
        }
    }
}
