using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class TypiconDBContextCommandExtensions
    {
        public static void UpdateOutputForm(this TypiconDBContext dbContext, OutputForm outputForm)
        {
            dbContext.Set<OutputForm>().Update(outputForm);

            dbContext.SaveChanges();
        }
        

        public static void UpdateModifiedYear(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            dbContext.SaveChanges();
        }

        public static Task UpdateModifiedYearAsync(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            return dbContext.SaveChangesAsync();
        }

        public static void UpdateTypiconVersion(this TypiconDBContext dbContext, TypiconVersion typiconVersion)
        {
            dbContext.Set<TypiconVersion>().FirstOrDefault(c => c.Id == typiconVersion.Id);

            dbContext.Set<TypiconVersion>().Update(typiconVersion);

            dbContext.SaveChanges();
        }

        public static void ClearOutputForms(this TypiconDBContext dbContext, int typiconId)
        {
            var outputForms = dbContext.Set<OutputForm>().Where(c => c.TypiconId == typiconId);

            dbContext.Set<OutputForm>().RemoveRange(outputForms);

            dbContext.SaveChanges();
        }

        public static void StartJob(this TypiconDBContext dbContext, JobBase job)
        {
            //ну вот что-то недо делать здесь
            job.BDate = DateTime.Now;
            job.Status = JobStatus.InProcess;
            dbContext.UpdateJob(job);
        }
        

        public static void UpdateJob(this TypiconDBContext dbContext, JobBase job)
        {
            //ну вот что-то недо делать здесь
        }

        public static void FailJob(this TypiconDBContext dbContext, JobBase job, string message)
        {
            //ну вот что-то недо делать здесь
            job.Status = JobStatus.Failed;
            job.StatusMessage = message;
            job.EDate = DateTime.Now;
            dbContext.UpdateJob(job);
        }

        public static void FinishJob(this TypiconDBContext dbContext, JobBase job)
        {
            //ну вот что-то недо делать здесь
            job.EDate = DateTime.Now;
            job.Status = JobStatus.Finished;
            dbContext.UpdateJob(job);
        }
    }
}
