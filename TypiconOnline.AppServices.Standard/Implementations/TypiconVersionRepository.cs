using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconVersionRepository : ITypiconVersionRepository
    {
        private readonly TypiconDBContext _context;

        public TypiconVersionRepository(TypiconDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Result<TypiconVersion> Get(int typiconVersionId, string login, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает опубликованную версию Устава.
        /// </summary>
        /// <param name="typiconId"></param>
        /// <returns></returns>
        public Result<TypiconVersion> GetPublishedVersion(int typiconId)
        {
            var version = _context.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == typiconId
                                    && c.BDate != null
                                    && c.EDate == null);

            return (version != null) ? Result.Ok(version) : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }
    }
}
