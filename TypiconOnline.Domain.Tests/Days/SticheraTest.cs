﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class SticheraTest
    {
        [Test]
        public void SticheraTest_Math()
        {
            int i = (int)Math.Ceiling((double) 8 / 3);

            Assert.AreEqual(i , 3);
        }

        [Test]
        public void SticheraTest_GetStichera_8_1()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            DayService dayService = _unitOfWork.Repository<DayService>().Get(c => c.Id == 350);

            TypiconSerializer ser = new TypiconSerializer();
            DayContainer container = ser.Deserialize<DayContainer>(dayService.DayDefinition);

            YmnosStructure stichera = container.Esperinos.Kekragaria.GetYmnosStructure(8, 1);

            Assert.AreEqual(stichera.YmnosStructureCount, 8);
        }

        [Test]
        public void SticheraTest_GetStichera_6_1()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            DayService dayService = _unitOfWork.Repository<DayService>().Get(c => c.Id == 350);

            TypiconSerializer ser = new TypiconSerializer();
            DayContainer container = ser.Deserialize<DayContainer>(dayService.DayDefinition);

            YmnosStructure stichera = container.Esperinos.Kekragaria.GetYmnosStructure(6, 1);

            Assert.AreEqual(stichera.YmnosStructureCount, 6);
        }

        [Test]
        public void SticheraTest_GetStichera_8_3()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            DayService dayService = _unitOfWork.Repository<DayService>().Get(c => c.Id == 350);

            TypiconSerializer ser = new TypiconSerializer();
            DayContainer container = ser.Deserialize<DayContainer>(dayService.DayDefinition);

            YmnosStructure stichera = container.Esperinos.Kekragaria.GetYmnosStructure(8, 3);

            Assert.AreEqual(stichera.YmnosStructureCount, 8);
        }

        [Test]
        public void SticheraTest_GetStichera_3_1()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            DayService dayService = _unitOfWork.Repository<DayService>().Get(c => c.Id == 350);

            TypiconSerializer ser = new TypiconSerializer();
            DayContainer container = ser.Deserialize<DayContainer>(dayService.DayDefinition);

            YmnosStructure stichera = container.Esperinos.Kekragaria.GetYmnosStructure(3, 1);

            Assert.AreEqual(stichera.YmnosStructureCount, 3);
        }

        [Test]
        public void SticheraTest_GetStichera_3_3()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            DayService dayService = _unitOfWork.Repository<DayService>().Get(c => c.Id == 350);

            TypiconSerializer ser = new TypiconSerializer();
            DayContainer container = ser.Deserialize<DayContainer>(dayService.DayDefinition);

            YmnosStructure stichera = container.Esperinos.Kekragaria.GetYmnosStructure(3, 3);

            Assert.AreEqual(stichera.YmnosStructureCount, 3);
        }
    }
}