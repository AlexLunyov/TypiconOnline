using NUnit.Framework;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class IntTest
    {
        [Test]
        public void Rules_Expressions_Int_WrongNumber()
        {
            string xmlString = "<int>--13-06</int>";

            var element = TestRuleSerializer.Deserialize<Int>(xmlString);
            element.Interpret(BypassHandler.Instance);

            Assert.AreEqual(0, element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_Int_RightNumber()
        {
            string xmlString = "<int>-15</int>";

            var unitOfWork = TestRuleSerializer.Create();

            var element = unitOfWork.Container<Int>()
                .Deserialize(new XmlDescriptor() { Description = xmlString }, null);

            Assert.IsNotNull(element);

            Assert.AreEqual(-15, element.ValueCalculated);
        }
    }
}
