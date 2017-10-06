using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsExistsTest
    {
        [Test]
        public void IsExists_Test()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;

            ServiceSequenceHandler handler = new ServiceSequenceHandler() { Settings = new RuleHandlerSettings() { Language = "cs-ru" } };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.GetXml("IsExists.xml");

            //Дата --01-16 exists - false
            DateTime date = new DateTime(2017, 01, 16);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayServices = rule.DayServices;

            rule.Rule.Interpret(date, handler);

            EktenisViewModel model = (rule.Rule as Ektenis).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.ChildElements.Count);

            //Дата --02-09 exists - true
            date = new DateTime(2017, 02, 09);

            rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayServices = rule.DayServices;

            rule.Rule.Interpret(date, handler);

            model = (rule.Rule as Ektenis).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.ChildElements.Count);
        }
    }
}
