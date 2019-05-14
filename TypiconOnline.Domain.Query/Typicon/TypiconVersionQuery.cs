using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconVersionQuery: IQuery<TypiconVersion>
    {
        public TypiconVersionQuery(int typiconVersionId)
        {
            TypiconVersionId = typiconVersionId;
        }

        public int TypiconVersionId { get; }
    }
}
