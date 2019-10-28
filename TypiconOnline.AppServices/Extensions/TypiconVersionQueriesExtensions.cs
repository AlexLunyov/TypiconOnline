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
    }
}
