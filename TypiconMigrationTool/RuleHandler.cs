using System;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconMigrationTool.Tests;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconMigrationTool
{
    public class RuleHandler
    {
        IUnitOfWork _unitOfWork;

        TypiconEntity _typiconEntity;

        IScheduleService _scheduleService;

        public RuleHandler(IUnitOfWork unitOfWork)
        {
            Initialize(unitOfWork);
        }

        private void Initialize(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");

            _typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            if (_typiconEntity == null)
                throw new NullReferenceException("TypiconEntity");

            var easterContext = new EasterFakeContext(new DateTime(2010, 4, 7));

            _scheduleService = ScheduleServiceFactory.Create(_unitOfWork, easterContext);
        }
    }
}
