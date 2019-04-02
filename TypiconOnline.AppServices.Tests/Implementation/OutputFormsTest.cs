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
using TypiconOnline.Domain.Rules.Output;
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
        public void OutputForms_Get()
        {
            var dbContext = TypiconDbContextFactory.Create();
            var outputForms = OutputFormsFactory.Create(dbContext);

            DeleteAllOutputForms(dbContext, TYPICON_ID);

            Result<LocalizedOutputDay> task = outputForms.Get(TYPICON_ID, new DateTime(2020, 1, 1), "cs-ru");

            Assert.IsTrue(task.Failure);
        }

        private void DeleteAllOutputForms(TypiconDBContext dbContext, int typiconId)
        {
            var forms = dbContext.Set<OutputForm>().Where(c => c.TypiconId == typiconId);

            dbContext.Set<OutputForm>().RemoveRange(forms);

            dbContext.SaveChanges();
        }
    }
}
