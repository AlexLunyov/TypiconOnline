using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class CommonRuleQuery : IQuery<CommonRule>
    {
        public CommonRuleQuery(int typiconId, string name)
        {
            TypiconId = typiconId;
            Name = name;
        }

        public int TypiconId { get; }
        public string Name{ get; }
    }
}
