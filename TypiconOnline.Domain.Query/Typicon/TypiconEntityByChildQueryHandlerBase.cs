using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public abstract class TypiconEntityByChildQueryHandlerBase<T> : DbContextQueryBase where T: RuleEntity, new()
    {
        public TypiconEntityByChildQueryHandlerBase(TypiconDBContext dbContext) : base(dbContext) { }

        protected Result<TypiconEntity> Handle([NotNull] TypiconEntityByChildQuery<T> query)
        {
            var found = DbContext.Set<T>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(found.TypiconVersion.Typicon);
            }
            else
            {
                return Result.Fail<TypiconEntity>("Устав для указанного объекта не был найден.");
            }
        }
    }
}
