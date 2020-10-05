using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Rules.Variables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Variables
{
    [TestFixture]
    public class WorshipVariablesTest
    {
        [Test]
        public void WorshipVariables_Test()
        {
            string xml = TestDataXmlReader.GetXmlString("WorshipVariableTest_Rule.xml");

            //получаем контейнер с VariableWorshipRules
            var ruleContainer = VariablesSerializerRoot.Container<RootContainer>().Deserialize(xml);

            var handler = new ScheduleHandler();

            //Интерпретируем
            ruleContainer.Interpret(handler);

            //в итоге получаем определенные worships
            var worships = handler.GetResults();

            Assert.AreEqual(1, worships.DayBefore.Count);
            Assert.AreEqual(2, worships.ThisDay.Count);
        }

        private RuleSerializerRoot VariablesSerializerRoot
        {
            get
            {
                string xml = TestDataXmlReader.GetXmlString("WorshipVariableTest_Worships.xml");

                var mockFactory = new Mock<IQueryProcessor>();
                mockFactory.Setup(c => c.Process((It.IsAny<TypiconVariableQuery>())))
                    .Returns(Result.Ok(new TypiconVariable() { Value = xml }));

                return new RuleSerializerRoot(mockFactory.Object, new TypiconSerializer());
            }
        }
    }
}
