using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.Jobs.Validation;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class TypiconVersionValidationTest
    {
        [Test]
        public void Validate_MadeByHands()
        {
            TypiconVersion version = new TypiconVersion()
            {
                Id = 1,
                //Name = new ItemText()
                //{
                //    Items = new List<ItemTextUnit>() { new ItemTextUnit("cs-ru", "Типикон") }
                //},
                Signs = new List<Sign>() { new Sign() { Id = 1 } }
            };

            var errs = version.GetBrokenConstraints(TestRuleSerializer.Create());

            Assert.Greater(errs.Count, 0);
        }

        [Test]
        public void Validate_FromDB()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var version = dbContext.Set<TypiconVersion>().First(c => c.Id == 1);

            var errs = version.GetBrokenConstraints(TestRuleSerializer.Create());

            Assert.AreEqual(errs.Count, 0);
        }

        [Test]
        public void ValidateMenologyRule()
        {
            var dbContext = TypiconDbContextFactory.Create();
            var menologyRule = dbContext.Set<MenologyRule>().FirstOrDefault();

            menologyRule.RuleDefinition = @"<rule><worship1></rule>";
            dbContext.Set<MenologyRule>().Update(menologyRule);
            dbContext.SaveChanges();

            var job = new ValidateMenologyRuleJob(menologyRule.Id);

            var jobRepo = new JobRepository(job);

            var processor = CommandProcessorFactory.CreateJobProcessor(dbContext, jobRepo);

            processor.ExecuteAsync(job);

            Assert.IsTrue(true);
        }
    }
}
