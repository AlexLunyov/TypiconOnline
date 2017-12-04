using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class AndTest
    {
        [Test]
        public void Rules_Expressions_And_Valid()
        {
            string xmlString = @"<and>
		                            <equals><int>1</int><int>1</int></equals>
		                            <more><int>2</int><int>1</int></more>
	                            </and>";

            var element = TestRuleSerializer.Deserialize<And>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_And_TypeBooleanMismatch()
        {
            string xmlString = @"<and>
		                            <equals><int>1</int><int>1</int></equals>
		                            <int>2</int>
	                            </and>";

            var element = TestRuleSerializer.Deserialize<And>(xmlString);
            //element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_And_True()
        {
            string xmlString = @"<and>
		                            <equals><int>1</int><int>1</int></equals>
		                            <more><int>2</int><int>1</int></more>
                                    <less><int>2</int><int>15</int></less>
	                            </and>";

            var element = TestRuleSerializer.Deserialize<And>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_And_False()
        {
            string xmlString = @"<and>
		                            <equals><int>1</int><int>1</int></equals>
		                            <more><int>2</int><int>12</int></more>
                                    <less><int>2</int><int>15</int></less>
	                            </and>";

            var element = TestRuleSerializer.Deserialize<And>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.IsFalse((bool)element.ValueCalculated);
        }
    }
}
