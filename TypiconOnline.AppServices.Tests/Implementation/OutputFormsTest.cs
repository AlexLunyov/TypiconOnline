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
using TypiconOnline.Domain.Typicon.Output;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    class OutputFormsTest
    {
        const int TYPICON_ID = 1;

        //[Test]
        //public void OutputForms_Get()
        //{
        //    var dbContext = TypiconDbContextFactory.Create();
        //    var outputForms = OutputFormsFactory.Create(dbContext);

        //    DeleteAllOutputDays(dbContext, TYPICON_ID);

        //    Result<LocalizedOutputDay> task = outputForms.GetDay(TYPICON_ID, new DateTime(2020, 1, 1), "cs-ru");

        //    Assert.IsTrue(task.Failure);
        //}

        private void DeleteAllOutputDays(TypiconDBContext dbContext, int typiconId)
        {
            var forms = dbContext.Set<OutputDay>().Where(c => c.TypiconId == typiconId);

            dbContext.Set<OutputDay>().RemoveRange(forms);

            dbContext.SaveChanges();
        }
    }
}
