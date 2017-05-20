using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class DayModificationTest
    {
        [Test]
        public void DayModification_Simple()
        {
            string xmlString = @"<daymodification daymove=""0""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            Assert.AreEqual(0, element.DayMoveCount.Value);
        }

        [Test]
        public void DayModification_WrongCustomName()
        {
            string xmlString = @"<daymodification daymove=""0""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    DayModification element = new DayModification(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException )
            //{
            //    Assert.Pass("isCustomname имеет неверное значение");
            //}
        }

        [Test]
        public void DayModification_Wrong_HasTwoTerms()
        {
            string xmlString = @"<daymodification daymove=""0""><date>--11-08</date></daymodification>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);

            //try
            //{
            //    DayModification element = new DayModification(xmlDoc.FirstChild);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    Assert.Pass(ex.Message);
            //}
        }

        [Test]
        public void DayModification_DefinitionsNotInterpretedException()
        {
            string xmlString = @"<daymodification><date>--11-08</date></daymodification>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

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
        public void DayModification_WithinDate()
        {
            string xmlString = @"<daymodification><date>--11-08</date></daymodification>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            //InterpretingSettings settings = new InterpretingSettings(InterpretingMode.ModificationDayOnly);

            //ModificationsRuleHandler handler = new ModificationsRuleHandler()

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            DateTime date = element.MoveDateCalculated;

            Assert.Pass(date.ToString("dd-MM-yyyy"));
        }

        [Test]
        public void DayModification_WithinWrongDate()
        {
            string xmlString = @"<daymodification><date>--13-08</date></daymodification>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void DayModification_NoDate()
        {
            string xmlString = @"<daymodification></daymodification>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DayModification element = new DayModification(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }
    }
}
