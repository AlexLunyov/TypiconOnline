using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class CommonRuleQueryHandler : DbContextQueryBase, IDataQueryHandler<CommonRuleQuery, CommonRule>
    {
        public CommonRuleQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public CommonRule Handle([NotNull] CommonRuleQuery query)
        {
            return DbContext.Set<CommonRule>().FirstOrDefault(c => c.TypiconVersionId == query.TypiconId && c.Name == query.Name);
        }
    }
}
