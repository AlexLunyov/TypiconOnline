using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Repository.EF;
using System.Linq;
using TypiconOnline.Domain.Books;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class EqualsTest
    {
        [Test]
        public void Rules_Expressions_Equals_Creature()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                    <int>2</int>
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_NotEnoughChildren()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_ChildrenRequired()
        {
            string xmlString = @"<equals>
                                    
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_InvalidChild()
        {
            string xmlString = @"<equals>
                                    <case>11</case>
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_TypeMismatch()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                    <getclosestday dayofweek=""saturday"" weekcount=""-2""><date>--11-08</date></getclosestday>
                                   </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            Assert.IsFalse(element.IsValid);
        }
        

       [Test]
        public void Rules_Expressions_And_Calculating_Correct()
        {
            string xmlString = @"<equals>
                                    <int>-1</int>
                                    <daysfromeaster><date>--04-15</date></daysfromeaster>
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);
            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 4, 15)));

            Assert.IsTrue((bool)element.ValueCalculated);
        }
        [Test]
        public void Rules_Expressions_And_Dates()
        {
            string xmlString = @"<equals>
                                    <date/>
                                    <date>--04-15</date>
                                 </equals>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 4, 15)));

            Assert.IsTrue((bool)element.ValueCalculated);
        }
    }
}
