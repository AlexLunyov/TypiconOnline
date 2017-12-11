﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class EktenisTest
    {
        //[Test]
        //public void EktenisTest_CalculatedElements()
        //{
        //    string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");

        //    FileReader reader = new FileReader(folderPath);
        //    string xml = reader.Read("Ektenis_CalculatedElements.xml");

        //    var element = TestRuleSerializer.Deserialize<EktenisRule>(xml);

        //    ServiceSequenceHandler handler = new ServiceSequenceHandler();

        //    handler.Settings = new RuleHandlerSettings() { Language = "cs-ru" };

        //    element.Interpret(DateTime.Today, handler);

        //    Assert.AreEqual(6, element.CalculatedElements.Count);
        //}

        //[Test]
        //public void EktenisTest_ViewModel()
        //{
        //    string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");

        //    FileReader reader = new FileReader(folderPath);
        //    string xml = reader.Read("Ektenis_CalculatedElements.xml");

        //    var element = TestRuleSerializer.Deserialize<EktenisRule>(xml);

        //    ServiceSequenceHandler handler = new ServiceSequenceHandler();

        //    handler.Settings = new RuleHandlerSettings() { Language = "cs-ru" };

        //    element.Interpret(DateTime.Today, handler);

        //    EktenisViewModel model = element.CreateViewModel(handler) as EktenisViewModel;

        //    Assert.AreEqual("deacon1", (model.ChildElements[1] as TextHolderViewModel).Paragraphs.First());
        //}
    }
}
