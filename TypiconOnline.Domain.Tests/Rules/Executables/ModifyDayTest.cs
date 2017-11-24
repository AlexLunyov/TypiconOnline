using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ModifyDayTest
    {
        [Test]
        public void ModifyDay_Simple()
        {
            string xmlString = @"<modifyday daymove=""0""/>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.AreEqual(0, element.DayMoveCount);
        }

        [Test]
        public void ModifyDay_Wrong_HasTwoTerms()
        {
            string xmlString = @"<modifyday daymove=""0""><date>--11-08</date></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    ModifyDay element = new ModifyDay(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void ModifyDay_DefinitionsNotInterpretedException()
        {
            string xmlString = @"<modifyday><date>--11-08</date></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            DateTime date = element.MoveDateCalculated;

            Assert.AreEqual(date, DateTime.MinValue);

            //try
            //{
            //    DateTime date = element.MoveDate;
            //}
            //catch (DefinitionsNotInterpretedException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void ModifyDay_WithinDate()
        {
            string xmlString = @"<modifyday><date>--11-08</date></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            //InterpretingSettings settings = new InterpretingSettings(InterpretingMode.ModificationDayOnly);

            //ModificationsRuleHandler handler = new ModificationsRuleHandler()

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            DateTime date = element.MoveDateCalculated;

            Assert.Pass(date.ToString("dd-MM-yyyy"));
        }

        [Test]
        public void ModifyDay_WithinWrongDate()
        {
            string xmlString = @"<modifyday><date>--13-08</date></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void ModifyDay_NoDate()
        {
            string xmlString = @"<modifyday></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void ModifyDay_DateBydaysFromEaster()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            BookStorage.Instance = BookStorageFactory.Create();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            string xmlString = @"<modifyday><datebydaysfromeaster><int>-43</int></datebydaysfromeaster></modifyday>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void ModifyDay_UseFullNameTest()
        {
            string xmlString = @"<modifyday daymove = ""0"" priority=""3""/>";

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.IsTrue(element.UseFullName);
        }
    }
}
