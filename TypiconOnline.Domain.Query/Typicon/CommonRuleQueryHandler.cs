using JetBrains.Annotations;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class CommonRuleQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<CommonRuleQuery, CommonRule>
    {
        public CommonRuleQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CommonRule Handle([NotNull] CommonRuleQuery query)
        {
            return UnitOfWork.Repository<CommonRule>().Get(c => c.OwnerId == query.TypiconId && c.Name == query.Name);
        }
    }
}
