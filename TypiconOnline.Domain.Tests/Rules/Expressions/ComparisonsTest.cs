using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Repository.EF;
using System.Linq;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Serialization;

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

        [Test]
        public void Rules_Expressions_More_False()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                    <int>-11</int>
                                    <daysfromeaster><date>--04-15</date></daysfromeaster>
                                 </more>";

            var element = TestRuleSerializer.Deserialize<BooleanExpression>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 4, 15)));

            Assert.IsFalse((bool)element.ValueCalculated);
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

            Assert.IsTrue((bool)element.ValueCalculated);
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

            Assert.IsTrue((bool)element.ValueCalculated);
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

            Assert.IsTrue((bool)element.ValueCalculated);
        }
    }
}
