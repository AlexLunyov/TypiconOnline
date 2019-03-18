using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class TypiconDBContextQueryExtensions
    {
        /// <summary>
        /// Возвращает опубликованную версию Устава.
        /// </summary>
        /// <param name="typiconId"></param>
        /// <returns></returns>
        public static Result<TypiconVersion> GetPublishedVersion(this TypiconDBContext dbContext, int typiconId)
        {
            var version = dbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == typiconId
                                    && c.BDate != null
                                    && c.EDate == null);

            return (version != null) 
                ? Result.Ok(version) 
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }

        public static Result<ScheduleDay> GetScheduleDay(this TypiconDBContext dbContext, int typiconId, DateTime date, ITypiconSerializer serializer)
        {
            var outputForm = dbContext.Set<OutputForm>().FirstOrDefault(c => c.TypiconId == typiconId && c.Date == date);

            if (outputForm != null)
            {
                var day = serializer.Deserialize<ScheduleDay>(outputForm.Definition);

                return Result.Ok(day);
            }

            return Result.Fail<ScheduleDay>("Выходная форма не найдена");
        }

        public static bool IsModifiedYearExists(this TypiconDBContext dbContext, int typiconVersionId, int year)
        {
            return dbContext.Set<ModifiedYear>().Any(c => c.TypiconVersionId == typiconVersionId && c.Year == year);
        }

        public static bool IsCalcModifiedYearExists(this TypiconDBContext dbContext, int typiconVersionId, int year)
        {
            return dbContext.Set<ModifiedYear>()
                .Any(c => c.TypiconVersionId == typiconVersionId 
                    && c.Year == year
                    && c.IsCalculated);
        }

        public static IEnumerable<MenologyRule> GetAllMenologyRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<MenologyRule>().Where(c => c.TypiconVersionId == typiconVersionId);
        }

        public static MenologyRule GetMenologyRule(this TypiconDBContext dbContext, int typiconVersionId, DateTime date)
        {
            return (DateTime.IsLeapYear(date.Year))
                ? dbContext.Set<MenologyRule>().FirstOrDefault(c => c.LeapDate.Day == date.Day && c.LeapDate.Month == date.Month)
                : dbContext.Set<MenologyRule>().FirstOrDefault(c => c.Date.Day == date.Day && c.Date.Month == date.Month);
        }

        public static IEnumerable<TriodionRule> GetAllTriodionRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<TriodionRule>().Where(c => c.TypiconVersionId == typiconVersionId);
        }

        public static TriodionRule GetTriodionRule(this TypiconDBContext dbContext, int typiconVersionId, DateTime date)
        {
            var currentEaster = dbContext.GetCurrentEaster(date.Year);

            int daysFromEaster = date.Date.Subtract(currentEaster.Date).Days;

            return dbContext.Set<TriodionRule>()
                .FirstOrDefault(c => c.TypiconVersionId == typiconVersionId && c.DaysFromEaster == daysFromEaster);
        }

        public static DateTime GetCurrentEaster(this TypiconDBContext dbContext, int year)
        {
            EasterItem easter = dbContext.Set<EasterItem>().FirstOrDefault(c => c.Date.Year == year);

            if (easter == null)
                throw new NullReferenceException($"День празднования Пасхи не определен для года {year}.");

            return easter.Date;
        }
    }
}
