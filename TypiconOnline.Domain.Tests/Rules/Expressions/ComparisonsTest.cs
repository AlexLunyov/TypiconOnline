using System;
using NUnit.Framework;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class ComparisonsTest
    {
        [Test]
        public void Rules_Expressions_More_Creature()
        {
            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                 </more>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_More_True()
        {
            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                    <int>-11</int>
                                 </more>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [TestCase(false, @"<more>
                            <int>2</int>
                            <int>1</int>
                            <int>-11</int>
                            <daysfromeaster><date>--04-15</date></daysfromeaster>
                           </more>")]
        [TestCase(true, @"<more>
                            <date>--04-06</date>
                            <date>--04-05</date>
                            <date>--01-01</date>
                          </more>")]
        public void Rules_Expressions_More(bool expected, string xmlString)
        {
            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 4, 15)));

            Assert.AreEqual(expected, element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_MoreEquals_True()
        {
            string xmlString = @"<moreequals>
                                    <int>2</int>
                                    <int>2</int>
                                    <int>2</int>
                                 </moreequals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_Less_True()
        {
            string xmlString = @"<less>
                                    <int>1</int>
                                    <int>2</int>
                                    <int>15</int>
                                 </less>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_LessEquals_True()
        {
            string xmlString = @"<lessequals>
                                    <int>1</int>
                                    <int>1</int>
                                    <int>1</int>
                                 </lessequals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.ValueCalculated);
        }
    }
}
