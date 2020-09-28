using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class TypiconVersionQueriesExtensions
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
                                    && c.BDate != null && c.EDate == null);

            return (version != null)
                ? Result.Ok(version)
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }

        public static IEnumerable<TypiconVersion> GetAllPublishedVersions(this TypiconDBContext dbContext)
        {
            return dbContext.Set<TypiconVersion>().Where(c => c.BDate != null && c.EDate == null);
        }

        /// <summary>
        /// Возвращает указанную версию Устава (опубликованную или черновик).
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        public static Result<TypiconVersion> GetTypiconVersion(this TypiconDBContext dbContext, int typiconId, TypiconVersionStatus status)
        {
            var version = dbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => (status == TypiconVersionStatus.Draft)
                                        ? c.TypiconId == typiconId && c.BDate == null && c.EDate == null
                                        : c.TypiconId == typiconId && c.BDate != null && c.EDate == null);

            return (version != null)
                ? Result.Ok(version)
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его версия.");
        }

        /// <summary>
        /// Возвращает указанную версию Устава (опубликованную или черновик).
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        public static Result<TypiconVersion> GetTypiconVersion(this TypiconDBContext dbContext, int typiconId)
        {
            var version = dbContext.Set<TypiconVersion>().FirstOrDefault(c => c.TypiconId == typiconId);

            return (version != null)
                ? Result.Ok(version)
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }

        /// <summary>
        /// Возвращает указанную версию Устава (опубликованную или черновик).
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        public static Result<TypiconVersion> GetDraftTypiconVersion(this TypiconDBContext dbContext, int typiconId)
        {
            var version = dbContext.Set<TypiconVersion>()
                .Where(TypiconVersion.IsDraft)
                .FirstOrDefault(c => c.TypiconId == typiconId);

            return (version != null)
                ? Result.Ok(version)
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }

        public static IEnumerable<string> Validate(this TypiconVersion version)
        {
            var err = new List<string>();

            //Проверяем Статус
            if (version.Typicon.Status == TypiconStatus.Approving
                || version.Typicon.Status == TypiconStatus.Publishing
                || version.Typicon.Status == TypiconStatus.Validating)
            {
                err.Add("Устав находится в состоянии, не подлежащем для публикации. ");
            }

            bool hasVariables = version.TypiconVariables.Any();
            bool hasEmptyPrintTemplates = version.PrintDayTemplates
                            .Any(c => c.PrintFile == null
                                   || c.PrintFile.Length == 0);

            //Не Шаблон и есть переменные или пустые печатные шаблоны - так нельзя публиковать
            if (!version.IsTemplate && (hasVariables || hasEmptyPrintTemplates))
            {
                if (hasVariables)
                {
                    err.Add("Устав должен быть либо определен как Шаблон, либо всем Переменным должны быть заданы значения. ");
                }

                if (hasEmptyPrintTemplates)
                {
                    err.Add("Устав должен быть либо определен как Шаблон, либо все Печатные шаблоны должны иметь загруженные файлы. ");
                }
            }

            /*
                - не шаблон
			    - Служба совершается не каждый день(не все дни недели)
                - Отсутствует печатного шаблона "по умолчанию"
             */
            if (!version.IsTemplate
               && version.ScheduleSettings == null)
            {
                err.Add("Устав должен быть определен как Шаблон, либо должен быть определен график богослужений. ");
            }

            /*
                - не шаблон
			    - Служба совершается не каждый день(не все дни недели)
                - Отсутствует печатного шаблона "по умолчанию"
             */
            if (!version.IsTemplate
               && !version.ScheduleSettings?.IsEveryday == false
               && version.PrintDayDefaultTemplate == null)
            {
                err.Add("Устав должен быть определен как Шаблон, либо должен быть определен Печатный шаблон по умолчанию. ");
            }


            return err;
        }
    }
}
