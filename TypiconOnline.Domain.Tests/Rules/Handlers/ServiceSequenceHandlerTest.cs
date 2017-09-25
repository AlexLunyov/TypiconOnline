﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Handlers
{
    [TestFixture]
    public class ServiceSequenceHandlerTest
    {
        [Test]
        public void ServiceSequenceHandler_Working()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = new DateTime(2018, 5, 21),//DateTime.Today,
                Mode = HandlingMode.AstronimicDay,
                Handler = new ServiceSequenceHandler(),
                TypiconEntity = typiconEntity
            };

            ScheduleService scheduleService = new ScheduleService();

            GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);
        }
    }
}