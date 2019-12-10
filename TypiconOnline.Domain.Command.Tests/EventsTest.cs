using NUnit.Framework;
using System.Linq;
using TypiconOnline.Domain.Events;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Rules.Tests
{
    [TestFixture]
    public class EventsTest
    {
        [Test]
        public void DBContextEvents_Test()
        {
            var dbContext = TypiconDbContextFactory.CreateWithEvents();

            var sign = dbContext.Set<Sign>().First();

            string xml = TestDataXmlReader.GetXmlString("PrintTemplateTest.xml");

            sign.ModRuleDefinition = xml;

            dbContext.SaveChanges();

            var template8 = dbContext.Set<PrintDayTemplate>()
                .FirstOrDefault(c => c.TypiconVersionId == sign.TypiconVersionId
                                    && c.Number == 8);

            var template10 = dbContext.Set<PrintDayTemplate>()
                .FirstOrDefault(c => c.TypiconVersionId == sign.TypiconVersionId
                                    && c.Number == 10);

            Assert.IsTrue(template8.SignPrintLinks.Where(c => c.EntityId == sign.Id).Count() == 1
                       && template10 != null);
        }
    }
}
