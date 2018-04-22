using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
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

            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var element = unitOfWork.Container<Int>()
                .Deserialize(new XmlDescriptor() { Description = xmlString }, null);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Int_RightNumber()
        {
            string xmlString = "<int>-15</int>";

            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var element = unitOfWork.Container<Int>()
                .Deserialize(new XmlDescriptor() { Description = xmlString }, null);

            Assert.IsNotNull(element);

            Assert.AreEqual(-15, element.ValueCalculated);
        }
    }
}
