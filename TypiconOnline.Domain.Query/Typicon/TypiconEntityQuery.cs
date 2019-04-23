using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconEntityQuery : IDataQuery<TypiconEntity>
    {
        public TypiconEntityQuery(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }
    }
}
