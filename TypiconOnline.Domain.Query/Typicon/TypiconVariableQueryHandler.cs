using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconVariableQueryHandler : DbContextQueryBase, IQueryHandler<TypiconVariableQuery, Result<TypiconVariable>>
    {
        public TypiconVariableQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconVariable> Handle([NotNull] TypiconVariableQuery query)
        {
            var r = DbContext.Set<TypiconVariable>().FirstOrDefault(c => c.TypiconVersionId == query.TypiconVersionId && c.Name == query.Name);

            return (r != null) 
                ? Result.Ok(r)
                : Result.Fail<TypiconVariable>($"Переменная с именем {query.Name} у версии Устава id={query.TypiconVersionId} не была найдена.");
        }
    }
}
