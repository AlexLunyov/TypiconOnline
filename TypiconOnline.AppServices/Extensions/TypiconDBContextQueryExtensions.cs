using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class TypiconDBContextQueryExtensions
    {
        public static Result<OutputDay> GetScheduleDay(this TypiconDBContext dbContext, int typiconId, DateTime date, ITypiconSerializer serializer)
        {
            var outputForm = dbContext.Set<OutputForm>().FirstOrDefault(c => c.TypiconId == typiconId && c.Date == date);

            if (outputForm != null)
            {
                var day = serializer.Deserialize<OutputDay>(outputForm.Definition);

                return Result.Ok(day);
            }

            return Result.Fail<OutputDay>("Выходная форма не найдена");
        }

        public static bool IsModifiedYearExists(this TypiconDBContext dbContext, int typiconVersionId, int year)
        {
            lock (dbContext)
            {
                return dbContext.Set<ModifiedYear>().Any(c => c.TypiconVersionId == typiconVersionId && c.Year == year);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="typiconVersionId"></param>
        /// <param name="year"></param>
        /// <returns>0 = не существует,
        /// 1 = существутет, но еще не вычислен,
        /// 2 = существует и вычислен</returns>
        public static int IsCalcModifiedYearExists(this TypiconDBContext dbContext, int typiconVersionId, int year)
        {
            var found = dbContext.Set<ModifiedYear>()
                .FirstOrDefault(c => c.TypiconVersionId == typiconVersionId && c.Year == year);

            int result = 0;

            if (found != null)
            {
                result = (found.IsCalculated) ? 2 : 1;
            }

            return result;
        }

        public static DateTime GetCurrentEaster(this TypiconDBContext dbContext, int year)
        {
            EasterItem easter = dbContext.Set<EasterItem>().FirstOrDefault(c => c.Date.Year == year);

            if (easter == null)
                throw new NullReferenceException($"День празднования Пасхи не определен для года {year}.");

            return easter.Date;
        }

        public static IEnumerable<TypiconVersionError> GetErrorsFromDb(this TypiconDBContext dbContext, int id)
        {
            return dbContext.Set<TypiconVersionError>().Where(c => c.TypiconVersionId == id);
        }

        public static Result<TypiconEntity> GetTypiconEntity(this TypiconDBContext dbContext, int id)
        {
            var found = dbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == id);

            if (found != null)
            {
                return Result.Ok(found);
            }

            return Result.Fail<TypiconEntity>($"Устав с заданным Id={id} не был найден.");
        }
    }
}
