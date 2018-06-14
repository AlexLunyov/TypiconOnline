﻿using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsExistsTest
    {
        [Test]
        public void IsExists_Test()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var serializer = TestRuleSerializer.Create(unitOfWork);

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru") }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("IsExists.xml");

            //Дата --01-16 exists - false
            DateTime date = new DateTime(2017, 01, 16);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            rule.GetRule<ExecContainer>(serializer).Interpret(handler);

            var model = handler.GetResult();

            //EktenisViewModel model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.FirstOrDefault()?.ChildElements.Count);

            //Дата --02-09 exists - true
            date = new DateTime(2017, 02, 09);

            rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            rule.GetRule<ExecContainer>(serializer).Interpret(handler);

            model = handler.GetResult();

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.FirstOrDefault()?.ChildElements.Count);
        }
    }
}
