using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    class OutputFormsTest
    {
        const int TYPICON_ID = 1;

        [Test]
        public async Task OutputForms_Get()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var outputForms = Build(dbContext);

            DeleteAllOutputForms(dbContext, TYPICON_ID);

            Result<ScheduleDay> task = outputForms.Get(TYPICON_ID, new DateTime(2020, 1, 1));

            Assert.IsTrue(task.Failure);
        }

        private void DeleteAllOutputForms(TypiconDBContext dbContext, int typiconId)
        {
            var forms = dbContext.Set<OutputForm>().Where(c => c.TypiconId == typiconId);

            dbContext.Set<OutputForm>().RemoveRange(forms);

            dbContext.SaveChanges();
        }

        private OutputForms Build(TypiconDBContext dbContext)
        {
            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var commandProcessor = CommandProcessorFactory.Create(dbContext);

            var nameComposer = new ScheduleDayNameComposer(serializerRoot.QueryProcessor);

            var modifiedYearFactory = new ModifiedYearFactory(dbContext, settingsFactory);

            var outputFormFactory = new OutputFormFactory(new ScheduleDataCalculator(serializerRoot.QueryProcessor, settingsFactory)
                , nameComposer
                , serializerRoot.TypiconSerializer);

            return new OutputForms(dbContext
            , new ScheduleDayNameComposer(serializerRoot.QueryProcessor)
            , serializerRoot.TypiconSerializer
            , outputFormFactory
            , new JobQueue());
        }
    }
}
