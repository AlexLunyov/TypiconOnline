﻿using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Migration;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsCelebratingTest : TypiconHavingTestBase
    {
        [Test]
        public void IsCelebrating_Test()
        {
            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Date = DateTime.Today }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("IsCelebrating_Simple.xml");

            //Ektenis element = RuleFactory.CreateElement(xml) as Ektenis;


            //находим Праздничное правило 
            MenologyRule rule = TypiconVersion.GetMenologyRule(new DateTime(2017, 09, 28));
            rule.RuleDefinition = xml;

            handler.Settings.Menologies = rule.DayWorships.ToList();

            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            var model = handler.ActualWorshipChildElements;            

            //var model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(3, model.Count);

            //а теперь находим правило НЕ праздничное
            rule = TypiconVersion.GetMenologyRule(new DateTime(2017, 10, 15));
            rule.RuleDefinition = xml;

            handler.Settings.Menologies = rule.DayWorships.ToList();

            handler.ClearResult();
            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            model = handler.ActualWorshipChildElements;

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.Count);
        }
    }
}
