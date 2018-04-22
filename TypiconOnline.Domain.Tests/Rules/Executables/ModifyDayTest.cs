using NUnit.Framework;
using System;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ModifyDayTest
    {
        [Test]
        public void ModifyDay_Simple()
        {
            string xmlString = @"<modifyday daymove=""0""/>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            Assert.AreEqual(0, element.DayMoveCount);
        }

        [Test]
        public void ModifyDay_Wrong_HasTwoTerms()
        {
            string xmlString = @"<modifyday daymove=""0""><date>--11-08</date></modifyday>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

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

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

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

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            //InterpretingSettings settings = new InterpretingSettings(InterpretingMode.ModificationDayOnly);

            //ModificationsRuleHandler handler = new ModificationsRuleHandler()

            element.Interpret(BypassHandler.Instance);

            DateTime date = element.MoveDateCalculated;

            Assert.Pass(date.ToString("dd-MM-yyyy"));
        }

        [Test]
        public void ModifyDay_WithinWrongDate()
        {
            string xmlString = @"<modifyday><date>--13-08</date></modifyday>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void ModifyDay_NoDate()
        {
            string xmlString = @"<modifyday></modifyday>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void ModifyDay_DateBydaysFromEaster()
        {
            string xmlString = @"<modifyday><datebydaysfromeaster><int>-43</int></datebydaysfromeaster></modifyday>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void ModifyDay_UseFullNameTest()
        {
            string xmlString = @"<modifyday daymove = ""0"" priority=""3""/>";

            var element = TestRuleSerializer.Deserialize<ModifyDay>(xmlString);

            Assert.IsTrue(element.UseFullName);
        }
    }
}
