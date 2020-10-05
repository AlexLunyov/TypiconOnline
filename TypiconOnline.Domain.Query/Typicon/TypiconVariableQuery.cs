using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconVariableQuery : IQuery<Result<TypiconVariable>>
    {
        public TypiconVariableQuery(int typiconVersionId, string name)
        {
            TypiconVersionId = typiconVersionId;
            Name = name;
        }

        public int TypiconVersionId { get; }
        public string Name{ get; }
    }
}
