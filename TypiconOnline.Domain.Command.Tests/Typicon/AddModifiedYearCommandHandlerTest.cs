using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Command.Tests.Typicon
{
    [TestClass]
    public class AddModifiedYearCommandHandlerTest : CommandTestBase
    {
        [TestMethod]
        public void AddModifiedYearCommandTest()
        {
            var typiconVersion = QueryProcessor.Process(new TypiconVersionQuery(1));

            var modifiedYear = new ModifiedYear()
            {
                TypiconVersionId = typiconVersion.Id,
                Year = 2019
            };

            Task result = CommandProcessor.ExecuteAsync(new AddModifiedYearCommand(modifiedYear));

            Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);
        }
    }
}
